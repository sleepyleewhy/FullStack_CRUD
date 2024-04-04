let teams = [];
let connection = null;
let teamToUpddate = -1;

fetchData();
SignalRSetup();

function SignalRSetup() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40930/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TeamCreated", (user, message) => {
        fetchData();
    });
    connection.on("TeamDeleted", (user, message) => {
        fetchData();
    });
    connection.on("TeamUpdated", (user, message) => {
        fetchData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function fetchData() {
    await fetch('http://localhost:40930/team')
        .then(x => x.json())
        .then(y => {
            teams = y;
            show();
        });
}
function show() {
    document.getElementById('teamtable').innerHTML = "";
    teams.forEach(t => {
        document.getElementById('teamtable').innerHTML +=
            "<tr><td>" + t.teamName + "</td><td>"
            + t.fanCount + "</td><td>" +
            t.divisionID + "</td><td>" +
            `<button id="deleteBtn" type="button" onclick="remove(${t.teamId})">Delete</button>` +
            `<button id="updateBtn" type="button" onclick="showupdate(${t.teamId})">Update</button>` +
            "</td></tr>";
    });
    console.log(teams);
}

function create() {
    let tname = document.getElementById('teamName').value;
    let tfan = document.getElementById('teamfancount').value;
    let tdivid= document.getElementById('teamdivid').value;

    fetch('http://localhost:40930/team', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            {
                teamName: tname,
                fanCount: tfan,
                divisionID: tdivid
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
    fetch('http://localhost:40930/team/' + id, {
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

    document.getElementById('teamupdateName').value = teams.find(t => t['teamId'] == id)['teamName'];
    document.getElementById('teamupdatefancount').value = teams.find(t => t['teamId'] == id)['fanCount'];
    document.getElementById('teamupdatedivid').value = teams.find(t => t['teamId'] == id)['divisionID'];
    document.getElementById('update').style.display = 'flex';
    teamToUpddate = id;
}

function update() {
    document.getElementById('update').style.display = 'none';
    let tname = document.getElementById('teamupdateName').value;
    let tfan = document.getElementById('teamupdatefancount').value;
    let tdivid = document.getElementById('teamupdatedivid').value;


    fetch('http://localhost:40930/team', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            teamId: teamToUpddate,
            teamName: tname,
            fanCount: tfan,
            divisionID: tdivid
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
