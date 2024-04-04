let divisions = [];
let connection = null;
let divisionToUpddate = -1;

fetchData();
SignalRSetup();

function SignalRSetup() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40930/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("DivisionCreated", (user, message) => {
        fetchData();
    });
    connection.on("DivisionDeleted", (user, message) => {
        fetchData();
    });
    connection.on("DivisionUpdated", (user, message) => {
        fetchData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function fetchData() {
    await fetch('http://localhost:40930/division')
        .then(x => x.json())
        .then(y => {
            divisions = y;
            show();
        });
}
function show() {
    document.getElementById('divisiontable').innerHTML = "";
    divisions.forEach(t => {
        document.getElementById('divisiontable').innerHTML +=
            "<tr><td>" + t.divisionName + "</td><td>"
            + t.population + "</td><td>" +
            `<button id="deleteBtn" type="button" onclick="remove(${t.divisionId})">Delete</button>` +
            `<button id="updateBtn" type="button" onclick="showupdate(${t.divisionId})">Update</button>` +
            "</td></tr>";
    });
}

function create() {
    let dname = document.getElementById('divisionName').value;
    let dpopulation = document.getElementById('divisionPopulation').value;

    fetch('http://localhost:40930/division', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            {
                divisionName: dname,
                population: dpopulation,
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
    fetch('http://localhost:40930/division/' + id, {
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

    document.getElementById('divisionupdateName').value = divisions.find(t => t['divisionId'] == id)['divisionName'];
    document.getElementById('divisionupdatePopulation').value = divisions.find(t => t['divisionId'] == id)['population'];
    document.getElementById('update').style.display = 'flex';
    divisionToUpddate = id;
}

function update() {
    document.getElementById('update').style.display = 'none';
    let dname = document.getElementById('divisionupdateName').value;
    let dpopulation = document.getElementById('divisionupdatePopulation').value;


    fetch('http://localhost:40930/division', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            divisionId: divisionToUpddate,
            divisionName: dname,
            population: dpopulation,
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
