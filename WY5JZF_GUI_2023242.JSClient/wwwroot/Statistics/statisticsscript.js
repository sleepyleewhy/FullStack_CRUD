let teams = [];
let divisions = [];
let statCollection = [];
let statone = '';
function initArrays() {
    getTeams();
    getDivisions();
}
function getTeams() {
    fetch('http://localhost:40930/team')
        .then(x => x.json())
        .then(y => {
            teams = y;
            console.log(teams);
        });
}
function getDivisions() {
    fetch('http://localhost:40930/division')
        .then(x => x.json())
        .then(y => {
            divisions = y;
            console.log(divisions);
        });
}
function showNone() {
    document.getElementById('AllPosPlayer_form').style.display = 'none';
    document.getElementById('AvgPerTeam_form').style.display = 'none';
    document.getElementById('Top3InDiv_form').style.display = 'none';
    document.getElementById('AllFansInDiv_form').style.display = 'none';
    document.getElementById('TeamWithMostSCost_form').style.display = 'none';
}


// AllPosPlayerPerTeam

function allpos() {
    showNone();
    document.getElementById('AllPosPlayer_form').style.display = 'initial';
    document.getElementById('AllPosPlayer_result').innerHTML = '';

    while (document.getElementById('allposplayer_selectTeam').firstChild) {
        document.getElementById('allposplayer_selectTeam').removeChild(document.getElementById('allposplayer_selectTeam').firstChild);
    }
    teams.forEach(function (x) {
        let item = document.createElement("option");
        item.text = x.teamName;
        item.value = x.teamName;
        document.getElementById('allposplayer_selectTeam').appendChild(item);
        console.log(item);
    });
}
function allpos_send()
{
    let team = document.getElementById('allposplayer_selectTeam').value;
    let chosenteam = teams.find(x => x.teamName == team)
    let pos = document.getElementById('selectPos').value;
    let path = 'http://localhost:40930/Stat/AllPosPlayerInTeam/' + pos + '/' + chosenteam.teamId;
    console.log(path);
    fetch(path)
        .then(x => x.json())
        .then(y => {
            statCollection = y;
            console.log(statCollection)
            console.log(typeof statCollection)
            allpos_show();
        });
}
function allpos_show() {
    let content = "";
    statCollection.forEach(t => {
        content += '<tr><td>' + t.playerName + "</td></tr>";
    });
    document.getElementById("AllPosPlayer_result").innerHTML = content;


}

// AvgPerTeam
function avgPerTeam() {
    showNone();
    document.getElementById('AvgPerTeam_form').style.display = 'initial';
    document.getElementById('AvgPerTeam_result').innerHTML = '';
    while (document.getElementById('avgperteam_selectTeam').firstChild) {
        document.getElementById('avgperteam_selectTeam').removeChild(document.getElementById('avgperteam_selectTeam').firstChild);
    }
    teams.forEach(function (x) {
        let item = document.createElement("option");
        item.text = x.teamName;
        item.value = x.teamName;
        document.getElementById('avgperteam_selectTeam').appendChild(item);
        console.log(item);
    });
}

 function avgperteam_send() {
    let team = document.getElementById('avgperteam_selectTeam').value;
    let chosenteam = teams.find(x => x.teamName == team)
    let path = 'http://localhost:40930/Stat/AvgPointsPerTeam/' + chosenteam.teamId;
    console.log(path);
    fetch(path)
        .then(x => x.json())
        .then(y => {
            statone = y;
            console.log(statone)
            console.log(typeof statone)
            document.getElementById("AvgPerTeam_result").innerHTML = Number(statone).toFixed(2);
        });
}

function top3inDiv() {
    showNone();
    document.getElementById('Top3InDiv_form').style.display = 'initial';
    document.getElementById('Top3InDiv_result').innerHTML = '';
    while (document.getElementById('top3indiv_selectDivision').firstChild) {
        document.getElementById('top3indiv_selectDivision').removeChild(document.getElementById('top3indiv_selectDivision').firstChild);
    };
    divisions.forEach(function (x) {
        let item = document.createElement("option");
        item.text = x.divisionName;
        item.value = x.divisionName;
        document.getElementById('top3indiv_selectDivision').appendChild(item);
        console.log(item);
    });
}
function top3indiv_send() {
    let divname = document.getElementById('top3indiv_selectDivision').value;

    let chosendiv = divisions.find(t => t.divisionName == divname);

    let path = 'http://localhost:40930/Stat/Top3PointsInDiv/' + chosendiv.divisionId;
    fetch(path)
        .then(x => x.json())
        .then(y => {
            statCollection = y;
            console.log(statCollection)
            console.log(typeof statCollection)
            top3indiv_show();
        });
}

function top3indiv_show() {
    let content = "";
    statCollection.forEach(t => {
        content += '<tr><td>' + t.playerName + "</td><td>" +t.avgPoints +  "</td></tr>";
    });
    document.getElementById("Top3InDiv_result").innerHTML = content;
}

// AllFansInDiv
function allFansInDiv() {
    showNone();
    document.getElementById('AllFansInDiv_form').style.display = 'initial';
    document.getElementById('AllFansInDiv_result').innerHTML = '';
    while (document.getElementById('allfansindiv_selectDivision').firstChild) {
        document.getElementById('allfansindiv_selectDivision').removeChild(document.getElementById('avgperteam_selectDivision').firstChild);
    }
    divisions.forEach(function (x) {
        let item = document.createElement("option");
        item.text = x.divisionName;
        item.value = x.divisionName;
        document.getElementById('allfansindiv_selectDivision').appendChild(item);
        console.log(item);
    });
}

function allfansindiv_send() {
    let divname = document.getElementById('allfansindiv_selectDivision').value;

    let chosendiv = divisions.find(t => t.divisionName == divname);

    let path = 'http://localhost:40930/Stat/AllFansPerDivision/' + chosendiv.divisionId;
    fetch(path)
        .then(x => x.json())
        .then(y => {
            statone = y;
            console.log(statone)
            console.log(typeof statone)
            document.getElementById("AllFansInDiv_result").innerHTML = Number(statone);
        });
}


//TeamWithMostSCost

function teamWithMostSCost() {
    showNone();
    document.getElementById('TeamWithMostSCost_form').style.display = 'initial';
    document.getElementById('TeamWithMostSCost_result').innerHTML = '';
    while (document.getElementById('teamwithmostscost_selectDivision').firstChild) {
        document.getElementById('teamwithmostscost_selectDivision').removeChild(document.getElementById('teamwithmostscost_selectDivision').firstChild);
    }
    divisions.forEach(function (x) {
        let item = document.createElement("option");
        item.text = x.divisionName;
        item.value = x.divisionName;
        document.getElementById('teamwithmostscost_selectDivision').appendChild(item);
        console.log(item);
    });
}

function teamwithmostscost_send() {
    let divname = document.getElementById('teamwithmostscost_selectDivision').value;

    let chosendiv = divisions.find(t => t.divisionName == divname);

    let path = 'http://localhost:40930/Stat/TeamWithMostSalaryCostInDiv/' + chosendiv.divisionId;
    fetch(path)
        .then(x => x.json())
        .then(y => {
            statone = y;
            console.log(statone)
            console.log(typeof statone)
            document.getElementById("TeamWithMostSCost_result").innerHTML = statone.teamName;
        });
}



