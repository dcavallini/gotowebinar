<%@ Page Language="C#" Inherits="iscirzioniWebinar.congratsWebinar" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Iscrizione avvenuta</title>      
    <!-- CSS only -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
    <link rel="stylesheet" href="style.css" type="text/css"/>
    <link rel="icon" type="image/jpg" href="imgs/icon.jpg" />

    <!-- JS, Popper.js, and jQuery -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js" integrity="sha384-B4gt1jrGC7Jh4AgTPSdUtOBvfO8shuf57BaghqFfPlYxofvL8/KUEfYiJOMMV+rV" crossorigin="anonymous"></script>
    <script src="script.js"></script>
</head>
<body style="background-color : lightgrey;">
    <div class="container" style="background-color : white;">
    <br>
        <a href="https://www.bluenext.it/" target="_blank">
            <img runat="server" src="imgs/blueNextNewLogo.png" style="width : 15em; heigth : auto;" class="ounded mx-auto d-block"/>
        </a>
        <br>
        <h1 class="text-center" style="color: blue;">Iscrizione avvenuta con successo!</h1>
        <h2 id="titoloWebinar" runat="server"></h2>
        <br>
        <p id="dataEOra" runat="server"></p>
        
        <div class="row-md-12">
<!--        <p style="margin: 0; padding: 0">All'ora indicata sopra, <a id="webinarLink" runat="server">collegarsi al webinar</a></p>-->
        <p style="margin: 0; padding: 0">Prima di partecipare, assicurati di <a href="https://support.goto.com/webinar/system-check-attendee?c_prod=g2w&c_name=confirmation&source=attendeeRegistrationPage&language=italian">verificare i requisiti minimi di sistema</a> per evitare qualsiasi problema di connessione</p>  <!---da prednere il link per le impostazioni audio-->
        <p style="margin: 0; padding: 0">&Egrave inviata un'email di conferma con informazioni sul metodo di partecipazione al webinar</p>
        <p style="margin: 0; padding: 0">Domande o commenti? Contattare formazione@bluenext.it</p>
    </div>
    </div>
    
    <!-- widget dell'aggiunta dell'evento al calendario-->
        
     <div class="footer container" style="background-color : white;">
       <div class="row-md-12">
            <p style="margin: 0; padding: 0"><a href="https://www.bluenext.it/" target="_blank">BlueNext Srl</a><img src="imgs/blueNextNewLogo.png" style="width : 13em; heigth : auto;" class="float-right"/></p>
            <p style="margin: 0; padding: 0">PI 04228480408</p>
            <p style="margin: 0; padding: 0">Viale XXIII Settembre 1845 n. 95</p>
            <p style="margin: 0; padding: 0">47921 Rimini (RN), Italia</p>
            <p style="margin: 0; padding: 0">TEL : 0541 328111  FAX : 0541 624964</p>
        </div>
    </div>
</body>
</html>
