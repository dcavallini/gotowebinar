<%@ Page Language="C#" Inherits="iscirzioniWebinar.Default" %>
<!DOCTYPE html>
<html>
    <head runat="server">
    	<title>Iscirione Webinar</title>
        
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
            <br>
            <h2 id="titoloWebinar" runat="server"></h2>
            <br>
            <p id="descrizioneWebinar" runat="server"></p>
            	<form id="form1" runat="server">
                <div class="row-md-12">
                    <h1 class="text-center"><asp:Literal id="titolo" runat="server"></asp:Literal></h1>
                </div>
                <br>
                <div class="row">
                    <div class="col-sm-6 my-1">
                        <label runat="server">Nome*</label>
                        <asp:TextBox runat="server" class="form-control" id="txtNome" placeholder="es. Mario"></asp:TextBox>
                    </div>
                    <div class="col-sm-6 my-1">
                        <label runat="server">Cognome*</label>
                        <asp:TextBox runat="server" class="form-control" id="txtCognome" placeholder="es. Rossi"></asp:TextBox>
                    </div>
                </div>
                <label runat="server">Email*</label>
                <div class="form-row align-items-center">
                    <div class="col-sm-12 my-1">
                    <label class="sr-only" for="txtMail" runat="server"></label>
                    <asp:TextBox runat="server" class="form-control" id="txtMail" placeholder="es. mario.rossi@gmail.com"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-6">
                        <label for="txtCF">Codice Fiscale (IMPORTANTE per ricevere i Crediti formativi)*</label>
                        <asp:TextBox class="form-control" id="txtCF" placeholder="es. RSSMRA20P16H294I" runat="server"/>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtTelefono">Cellulare</label>
                        <asp:TextBox class="form-control"  id="txtTelefono" placeholder="" runat="server"/>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-6">
                        <label for="txtRagioneSociale">Ragione Sociale</label>
                        <asp:TextBox class="form-control" id="txtRagioneSociale" placeholder="es. Studio Rossi Snc" runat="server"/>
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtPIVA">P. IVA</label>
                        <asp:TextBox class="form-control" id="txtPIVA" placeholder="es. 11111111111" runat="server"/>
                    </div>
                </div>
                <div class="row"> <!-- devo capire se devo fare un controllo anche su questo radio per abilitare il form al submit -->
                    <div class="col-md-6">
                        <label>Ricevere crediti formativi &egrave una cosa di tuo interesse?</label>
                    </div>
                    <div class="col-md-4">
                        <asp:RadioButton class="btn btn-default" id="radioSi" runat="server" OnCheckedChanged="radioSi_CheckedChanged" AutoPostBack=true Text="Si"></asp:RadioButton>
                        
                        <asp:RadioButton  class="btn btn-default" id="radioNo" runat="server" OnCheckedChanged="radioNo_CheckedChanged" AutoPostBack=true Text="No"></asp:RadioButton>                       
                    </div>
                </div>
                <div id="siCrediti" runat="server">
                    <div class="row">
                    <div class="form-group col-md-4">
                        <label for="inputOrdine">Ordine di appertenenza</label>
                        <asp:DropDownList id="inputOrdine" class="form-control" runat="server"/>            
                    </div>
                    <div class="form-group col-md-4">
                        <label>Inserisci il numero dell'ordine :</label>
                        <asp:TextBox id="txtNumeroOrdine" runat="server" class="form-control"></asp:TextBox>            
                    </div>
                    </div>
                </div>
                <div id="noCrediti" runat="server">
                    <div class="row">
                        <div class="form-group col-md-4">
                        <label for="inputProvincia">Provincia</label>
                        <asp:DropDownList id="inputProvincia" class="form-control" runat="server"/>            
                    </div>
                    </div>
                </div>
                <div class="row-md-12">
                    <div class="custom-control custom-checkbox mb-3">
                        <input type="checkbox" class="custom-control-input" Text="" id="checkPrivacy" runat="server" onchange="javascript:$('#btnSubmit').prop('disabled', !$('#btnSubmit').is(':disabled'));">
                        <label class="custom-control-label" for="checkPrivacy">Ho preso visione della nota <a href='https://fondoperduto.bluenext.it/fondoperduto/Risorse/informativaprivacy.pdf'>Informativa sulla Privacy</a>. Se fai clic su questo pulsante, i tuoi dati saranno inviati all'organizzatore del webinar che li utilizzer&agrave per le comunicazioni relative a questo evento e ad altri suoi servizi.*</label>
                        <div class="invalid-feedback">Campo obbligatorio</div>
                    </div>
                </div>
                <div class="row-md-12">
                    <p class="min-text">*Campo obbligatorio</p>
                </div>
                <div class="col text-center">
                    <asp:Button id="btnSubmit" OnClick="btnSubmit_Click" Text="Iscriviti" class="btn btn-primary" runat="server" Enabled=false/>
                </div>
            </form>
        </div>
        <div class="footer container" style="background-color : white;">
           <div class="row-md-12">
                    <p style="margin: 0; padding: 0"><a href="https://www.bluenext.it/" target="_blank">BlueNext Srl</a><img src="imgs/blueNextNewLogo.png" style="width : 15em; heigth : auto;" class="float-right"/></p>
                    <p style="margin: 0; padding: 0">PI 04228480408</p>
                    <p style="margin: 0; padding: 0">Viale XXIII Settembre 1845 n. 95</p>
                    <p style="margin: 0; padding: 0">47921 Rimini (RN), Italia</p>
                    <p style="margin: 0; padding: 0">TEL : 0541 328111</p>
                    <p style="margin: 0; padding: 0">FAX : 0541 624964</p>
            </div>
        </div>
    </body>
</html>
