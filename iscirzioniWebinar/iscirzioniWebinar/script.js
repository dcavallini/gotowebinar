function changeValue() {
    if (document.getElementById("checkPrivacy").checked) {
        document.getElementById("btnSubmit").disabled = false;
    }
    else {
        document.getElementById("btnSubmit").disabled = true;
    }
}

function valueSi() {
    if (document.getElementById("radioSi").checked) {
        document.getElementById("siCrediti").style.display = "initial";
        document.getElementById("noCrediti").style.display = "none";
        document.getElementById("radioNo").checked = false;
    }
}

function valueNo() {
    if (document.getElementById("radioNo").checked) {
        document.getElementById("noCrediti").style.display = "initial";
        document.getElementById("siCrediti").style.display = "none";
        document.getElementById("radioSi").checked = false;
    }
}

function disableDivs() {
    document.getElementById("siCrediti").style.display = "none";
    document.getElementById("noCrediti").style.display = "none";
}

//javascript:$('#btnSubmit').prop('disabled', !$('#btnSubmit').is(':disabled'));

//OnCheckedChanged="radioSi_CheckedChanged" AutoPostBack=true

//OnCheckedChanged="radioNo_CheckedChanged" AutoPostBack=true