let players = [];
let connection = null;
let playerToUpddate = -1;

fetchData();
SignalRSetup();

function SignalRSetup() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40930/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PlayerCreated", (user, message) => {
        fetchData();
    });
    connection.on("PlayerDeleted", (user, message) => {
        fetchData();
    });
    connection.on("PlayerUpdated", (user, message) => {
        fetchData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function fetchData() {
    await fetch('http://localhost:40930/player')
        .then(x => x.json())
        .then(y => {
            players = y;
            show();
        });
}
function show() {
    document.getElementById('playertable').innerHTML = "";
    players.forEach(t => {
        document.getElementById('playertable').innerHTML +=
            "<tr><td>" + t.playerName + "</td><td>"
            + t.position + "</td><td>" +
            t.salary + "</td><td>" +
            t.teamID + "</td><td>" +
            t.avgPoints + "</td><td>" +
            `<button id="deleteBtn" type="button" onclick="remove(${t.playerId})">Delete</button>` +
            `<button id="updateBtn" type="button" onclick="showupdate(${t.playerId})">Update</button>` +
            "</td></tr>";
    });
}

function create() {
    let pname = document.getElementById('playerName').value;
    let ppos = document.getElementById('playerPosition').value;
    let psalary = document.getElementById('playerSalary').value;
    let pteamid = document.getElementById('playerTeamID').value;
    let pavgpoints = document.getElementById('playerAvgPoints').value;

    fetch('http://localhost:40930/player', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            {
                playerName: pname,
                position: ppos,
                salary: psalary,
                teamID: pteamid,
                avgPoints: pavgpoints
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success', data);
            fetchData();
        })
        .catch((error) => {
            console.error('Error:', error);
        })
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

function remove(id) {
    fetch('http://localhost:40930/player/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            fetchData();
        })
        .catch((error) => { console.error('Error:', error); });
}
function showupdate(id) {

    document.getElementById('playerupdateName').value = players.find(t => t['playerId'] == id)['playerName'];
    document.getElementById('playerupdatePosition').value = players.find(t => t['playerId'] == id)['position'];
    document.getElementById('playerupdateSalary').value = players.find(t => t['playerId'] == id)['salary'];
    document.getElementById('playerupdateTeamID').value = players.find(t => t['playerId'] == id)['teamID'];
    document.getElementById('playerupdateAvgPoints').value = players.find(t => t['playerId'] == id)['avgPoints'];
    document.getElementById('update').style.display = 'flex';
    playerToUpddate = id;
}

function update() {
    document.getElementById('update').style.display = 'none';
    let pname = document.getElementById('playerupdateName').value;
    let ppos = document.getElementById('playerupdatePosition').value;
    let psalary = document.getElementById('playerupdateSalary').value;
    let pteamid = document.getElementById('playerupdateTeamID').value;
    let pavgpoints = document.getElementById('playerupdateAvgPoints').value; 


    fetch('http://localhost:40930/player', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            playerId: playerToUpddate,
            playerName: pname,
            position: ppos,
            salary: psalary,
            teamID: pteamid,
            avgPoints: pavgpoints
        })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            fetchData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}
