using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using LogMeIn.GoToCoreLib.Api;
using Newtonsoft.Json;
using RestSharp;

namespace iscirzioniWebinar
{
    public partial class Default : System.Web.UI.Page
    {
        protected string access_token = "eyJraWQiOiJvYXV0aHYyLmxtaS5jb20uMDIxOSIsImFsZyI6IlJTNTEyIn0.eyJzYyI6ImNvbGxhYjoiLCJscyI6IjY4NTkzN2M1LTE1ZGItNDIwMS05NGUwLTVkNjM0MmVlYzQ4NCIsIm9nbiI6InB3ZCIsImF1ZCI6ImRkMWQ3NzZmLTM3ODgtNGQ2MS05YmQxLTUyMGNkNWQ5MTU0ZiIsInN1YiI6IjYzMDE5MzM3MTY5ODEwNzgwMjIiLCJqdGkiOiJkN2U3ZTlhNS1hNmZkLTQyNzMtODk3NS03NGRiYTg1YWM4ZmQiLCJleHAiOjE2MDE5MTUyNjYsImlhdCI6MTYwMTkxMTY2NiwidHlwIjoiYSJ9.i2WL47NjgoD-_0g06eFpYoC49hKezZhjv2Ccj2L4Yk31nGHTmGnBl9G38EyV-39pvtQEspkDxlnDdUjGiLNUSTJByZr39VbktS2EbQIwuDL8_RmW-Xo6v8TRafIxQkx77v7rNvxvL-7ljQfw2vib4h5WA3kTIdnRM5WXFkIJUSEs3koV8Nx-l-8T4Yb-t7IyTWCFyf29U-QfjKbLDxjf1vH77Jb_MXLWQ_xcMRQxOQQV1LSCsjQunm3kUia99CgMX827WpTYvopgp6cis_q0YuO1Mq_TLMjqHd5PQsnvzKs6YxTjFGqJw_CF5eXWzCeCxxXtNxKCyKuSoArwsC-_3Q";
        protected string refresh_token = "eyJraWQiOiJvYXV0aHYyLmxtaS5jb20uMDIxOSIsImFsZyI6IlJTNTEyIn0.eyJzYyI6ImNvbGxhYjoiLCJscyI6IjY4NTkzN2M1LTE1ZGItNDIwMS05NGUwLTVkNjM0MmVlYzQ4NCIsIm9nbiI6InB3ZCIsImF1ZCI6ImRkMWQ3NzZmLTM3ODgtNGQ2MS05YmQxLTUyMGNkNWQ5MTU0ZiIsInN1YiI6IjYzMDE5MzM3MTY5ODEwNzgwMjIiLCJqdGkiOiI5YTE4NjA5NS1lOTMyLTRmMGItYjM4My00NGRhMjcxNmI5ZjYiLCJleHAiOjE2MDQ1MDM2NjYsImlhdCI6MTYwMTkxMTY2NiwidHlwIjoiciJ9.RmJNu_WSyeonSIckc0yEu9HTeDU9zlyvIaURq_81t0x7nbCSS0kLajpkE0BYEsG3y9_-aRu9NixeMznWqPdHZ0AvBM21WlVTqyHl1wQP-n4WzILExwzAIJyTK_khEF-9VngdbcksqJMsErPsBxkSJqOaU4jQ_3obNHp8Xru2FM9BvXFHRjlafqO-Tx95ZYjI5NjeKOfIcjBP92cpgMEiod7jVCwdfrFJIT0V_IUG344y_jIbYILNateDWHGO7xWlJWbmbjMzuEsFPHWbUZlNEorx6TDkHUFeuJKOkg06IsewA-1PZxmTPmpUMs8Hivhx8Hic1C7tKIEZ3BmZuVf5zw"; // => "d1cp20yB3hrFAKeTokenTr49EZ34kTvNK"
        protected string account_key = "1331643080583618309";
        protected string organizer_key = "6301933716981078022";
        //protected string webinarID;
        protected string webinarKey;
        protected Webinar webinar;
        protected string pipedrive_token = "ca504e05a6054a8f0b982b643a830e604b30ad0d"; //token che va cambiato a seconda di quale account si utilizza

        public void Page_Load(object sender, EventArgs args)
        {
            LocalitaItaliane li = new LocalitaItaliane();
            inputProvincia.DataSource = li.GetProvincie();
            inputProvincia.DataBind();

            inputOrdine.DataSource = li.GetProvincie();
            inputOrdine.DataBind();

            try
            {
                //webinarID = Request.QueryString["webinar_id"];
                webinarKey = Request.QueryString["webinar_key"];

                RefreshTokenGTW();

                webinar = GetWebinar();

                titoloWebinar.InnerText = webinar.subject;

                descrizioneWebinar.InnerText = webinar.description;

            }
            catch (Exception ex)//se l'iscrizione al webinar avviene correttamente devo comunque passare alla pagina oppure no?!?!
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void btnSubmit_Click(object sender, EventArgs args)
        {
            //solo se tutti i campi sono compilati correattamente allora is può fare la chiamata alla api
            if (Controlli())
            {
                try
                {
                    //devo fare un check se la mail risultà già iscritta a quel webinar
                    string registrant = CreateRegistrantForWebinar(webinar.webinarKey);

                    //GetOrganizationsPipeDrive();

                    Response.Redirect("congratsWebinar.aspx?subject=" + webinar.subject + "&start=" + webinar.times[0].startTime + "&end=" + webinar.times[0].endTime);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                checkPrivacy.Checked = false;
            }

        }

        private bool Controlli()//da aggiungere il controllo sulla dpr(maybe)
        {
            ControlloNome();
            ControlloCognome();
            ControlloMail();
            //ControlloPrivacy();
            ControlloCellulare();
            ControlloPIVA();
            ControlloCF();

            if (txtNome.Text == "" || txtCognome.Text == "" || txtMail.Text == "" || txtCF.Text == "" || !checkPrivacy.Checked || inputProvincia.Text == "" || !isEmail(txtMail.Text))
            {
                radioNo.Checked = false;
                radioSi.Checked = false;
                return false;
            }
            else if (!VerificaCodiceFiscale(txtCF.Text))
            {
                radioNo.Checked = false;
                radioSi.Checked = false;
                return false;
            }
            else if (txtPIVA.Text != "")
            {
                if (!VerificaPartitaIva(txtPIVA.Text))
                {
                    radioNo.Checked = false;
                    radioSi.Checked = false;
                    return false;
                }
            }
            else if (txtTelefono.Text != "")
            {
                if (!Cellulare.CheckLunghezza(txtTelefono.Text) || !Cellulare.CheckNumeri(txtTelefono.Text))
                {
                    radioNo.Checked = false;
                    radioSi.Checked = false;
                    return false;
                }
            }

            return true;
        }

        private void ControlloNome()
        {
            //if che controlla che il campo del nome sia inserito
            if (txtNome.Text == "")
            {
                //rimozione della class "is-valid" se il nome dovessere essere vuoto
                txtNome.Attributes.Add("class", String.Join(" ", txtNome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .ToArray()
                ));

                //aggiunta della class "is-invalid" se il nome è vuoto
                txtNome.Attributes.Add("class", String.Join(" ", txtNome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .Concat(new string[] { "is-invalid" })
                    .ToArray()
                ));

                txtNome.Attributes.Add("placeholder", "Nome mancante");

                //aggiunta della class "danger-color" se il nome è vuoto
                txtNome.Attributes.Add("class", String.Join(" ", txtNome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "danger-color" })
                    .Concat(new string[] { "danger-color" })
                    .ToArray()
                ));

            }
            else
            {
                //rimozione della class "is-invalid" se il campo del nome è stato compilato
                txtNome.Attributes.Add("class", String.Join(" ", txtNome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .ToArray()
                ));

                //aggiunta della class "is-valid" se il campo del nome è stato compilato
                txtNome.Attributes.Add("class", String.Join(" ", txtNome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .Concat(new string[] { "is-valid" })
                    .ToArray()
                ));

                txtNome.Attributes.Remove("placeholder");
            }
        }

        private void ControlloCognome()
        {
            //if che controlla che il campo del cognome si inserito
            if (txtCognome.Text == "")
            {
                //rimozione della class "is-valid" se il campo del cognome non dovesse essere compilato
                txtCognome.Attributes.Add("class", String.Join(" ", txtCognome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .ToArray()
                ));

                //aggiunta della class "is-invalid" se il campo del cognome non dovesse essere compilato
                txtCognome.Attributes.Add("class", String.Join(" ", txtCognome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .Concat(new string[] { "is-invalid" })
                    .ToArray()
                ));

                txtCognome.Attributes.Add("placeholder", "Cognome mancante");

                //aggiunta della class "danger-color" se il campo del cognome fosse vuoto
                txtCognome.Attributes.Add("class", String.Join(" ", txtCognome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "danger-color" })
                    .Concat(new string[] { "danger-color" })
                    .ToArray()
                ));

            }
            else
            {
                //rimozione della class "is-invalid" se il campo del cognome dovesse essere compilato
                txtCognome.Attributes.Add("class", String.Join(" ", txtCognome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .ToArray()
                ));

                //aggiunta della class "is-valid" se il campo del cognome dovesse essere compilato
                txtCognome.Attributes.Add("class", String.Join(" ", txtCognome
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .Concat(new string[] { "is-valid" })
                    .ToArray()
                ));

                txtCognome.Attributes.Remove("placeholder");
            }
        }

        private void ControlloMail()
        {
            //if che controlla se il campo complessivo della mail stato compilato (campo_mail = txtNomeMial + txtGruppoMail)
            if (txtMail.Text == "" || !isEmail(txtMail.Text)) 
            {
                //rimozione della class "is-valid" per la prima parte della mail se uno dei due campi dovessero essere vuoti
                txtMail.Attributes.Add("class", String.Join(" ", txtMail
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .ToArray()
                ));

                //aggiunta della class "is-invalid" per la prima parte della mail se uno dei due campi dovessero essere vuoti
                txtMail.Attributes.Add("class", String.Join(" ", txtMail
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .Concat(new string[] { "is-invalid" })
                    .ToArray()
                ));

                txtMail.Attributes.Add("placeholder", "Email mancante");

                //aggiunta della class "danger-color" per la prima parte della mail se uno dei due campi dovessero essere vuoti
                txtMail.Attributes.Add("class", String.Join(" ", txtMail
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "danger-color" })
                    .Concat(new string[] { "danger-color" })
                    .ToArray()
                ));

            }
            else
            {
                //rimozione della class "is-invalid" per la prima parte della mail se i due campi sono stati compilati
                txtMail.Attributes.Add("class", String.Join(" ", txtMail
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .ToArray()
                ));

                //aggiunta della class "is-valid" per la prima parte della mail se i due campi sono stati compilati
                txtMail.Attributes.Add("class", String.Join(" ", txtMail
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .Concat(new string[] { "is-valid" })
                    .ToArray()
                ));

                txtMail.Attributes.Remove("placeholder");
            }

        }

        //private void ControlloPrivacy()
        //{
        //    //controllo se la check box della presa visione della privacy sia checcata
        //    if (checkPrivacy.Checked)
        //    {
        //        //rimozione dell'is-invalid se la checkbox è checcata
        //        checkPrivacy.Attributes.Add("class", String.Join(" ", checkPrivacy
        //            .Attributes["class"]
        //            .Split(' ')
        //            .Except(new string[] { "", "is-invalid" })
        //            .ToArray()
        //        ));

        //        //aggiunta dell'is-valid se la checkbox è checcata
        //        checkPrivacy.Attributes.Add("class", String.Join(" ", checkPrivacy
        //            .Attributes["class"]
        //            .Split(' ')
        //            .Except(new string[] { "", "is-valid" })
        //            .Concat(new string[] { "is-valid" })
        //            .ToArray()
        //        ));
        //    }
        //    else
        //    {
        //        //rimozione dell'is-valid se la checkbox non è checcata
        //        checkPrivacy.Attributes.Add("class", String.Join(" ", checkPrivacy
        //            .Attributes["class"]
        //            .Split(' ')
        //            .Except(new string[] { "", "is-valid" })
        //            .ToArray()
        //        ));

        //        //aggiunta dell'is-invalid se la checkbox non è checcata
        //        checkPrivacy.Attributes.Add("class", String.Join(" ", checkPrivacy
        //            .Attributes["class"]
        //            .Split(' ')
        //            .Except(new string[] { "", "is-invalid" })
        //            .Concat(new string[] { "is-invalid" })
        //            .ToArray()
        //        ));
        //    }
        //}

        private void ControlloCellulare()
        {
            if(txtTelefono.Text != string.Empty)
            {
                if (Cellulare.CheckLunghezza(txtTelefono.Text) && Cellulare.CheckNumeri(txtTelefono.Text))
                {
                    //rimozione dell'is-invalid se il numero di cellulare è giusto
                    txtTelefono.Attributes.Add("class", String.Join(" ", txtTelefono
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-invalid" })
                        .ToArray()
                    ));

                    //aggiunta dell'is-valid se il numero di cellulare è giusto
                    txtTelefono.Attributes.Add("class", String.Join(" ", txtTelefono
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-valid" })
                        .Concat(new string[] { "is-valid" })
                        .ToArray()
                    ));

                    txtMail.Attributes.Remove("placeholder");
                }
                else
                {
                    //rimozione dell'is-valid se il numero di cellulare è troppo corto o non è un numero
                    txtTelefono.Attributes.Add("class", String.Join(" ", txtTelefono
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-valid" })
                        .ToArray()
                    ));

                    //aggiunta dell'is-invalid se il numero di cellulare è troppo corto o non è un numero
                    txtTelefono.Attributes.Add("class", String.Join(" ", txtTelefono
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-invalid" })
                        .Concat(new string[] { "is-invalid" })
                        .ToArray()
                    ));

                    txtTelefono.Attributes.Add("placeholder", "Numero di cellulare sbagliato");

                    //aggiunta della class "danger-color" per la prima parte della mail se uno dei due campi dovessero essere vuoti
                    txtTelefono.Attributes.Add("class", String.Join(" ", txtTelefono
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "danger-color" })
                        .Concat(new string[] { "danger-color" })
                        .ToArray()
                    ));

                }
            }
        }

        private void ControlloPIVA()
        {
            if (txtPIVA.Text != string.Empty)
            {
                if (VerificaPartitaIva(txtPIVA.Text))
                {
                    //rimozione dell'is-invalid se il numero di cellulare è giusto
                    txtPIVA.Attributes.Add("class", String.Join(" ", txtPIVA
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-invalid" })
                        .ToArray()
                    ));

                    //aggiunta dell'is-valid se il numero di cellulare è giusto
                    txtPIVA.Attributes.Add("class", String.Join(" ", txtPIVA
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-valid" })
                        .Concat(new string[] { "is-valid" })
                        .ToArray()
                    ));

                    txtPIVA.Attributes.Remove("placeholder");
                }
                else
                {
                    //rimozione dell'is-valid se il numero di cellulare è troppo corto o non è un numero
                    txtPIVA.Attributes.Add("class", String.Join(" ", txtPIVA
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-valid" })
                        .ToArray()
                    ));

                    //aggiunta dell'is-invalid se il numero di cellulare è troppo corto o non è un numero
                    txtPIVA.Attributes.Add("class", String.Join(" ", txtPIVA
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "is-invalid" })
                        .Concat(new string[] { "is-invalid" })
                        .ToArray()
                    ));

                    txtPIVA.Text = string.Empty;

                    txtPIVA.Attributes.Add("placeholder", "Partitva IVA sbagliata");

                    //aggiunta della class "danger-color" per la prima parte della mail se uno dei due campi dovessero essere vuoti
                    txtPIVA.Attributes.Add("class", String.Join(" ", txtPIVA
                        .Attributes["class"]
                        .Split(' ')
                        .Except(new string[] { "", "danger-color" })
                        .Concat(new string[] { "danger-color" })
                        .ToArray()
                    ));
                }
            }
        }

        private void ControlloCF()
        {
            //if(CodiceFiscale.ControlloFormaleOK(txtCF.Text.ToUpper()))
            if(VerificaCodiceFiscale(txtCF.Text))
            {
                //rimozione della class "is-invalid" se il codice fiscale dovessere essere giusto
                txtCF.Attributes.Add("class", String.Join(" ", txtCF
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .ToArray()
                ));

                //aggiunta della class "is-valid" se il codice fiscale è corretto
                txtCF.Attributes.Add("class", String.Join(" ", txtCF
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .Concat(new string[] { "is-valid" })
                    .ToArray()
                ));

                txtCF.Attributes.Remove("placeholder");
            }
            else
            {
                //rimozione della class "is-valid" se il codice fiscale dovessere essere sbagliato
                txtCF.Attributes.Add("class", String.Join(" ", txtCF
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-valid" })
                    .ToArray()
                ));

                //aggiunta della class "is-invalid" se il codice fiscale è vuoto
                txtCF.Attributes.Add("class", String.Join(" ", txtCF
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "is-invalid" })
                    .Concat(new string[] { "is-invalid" })
                    .ToArray()
                ));

                txtCF.Text = string.Empty;

                txtCF.Attributes.Add("placeholder", "Codice Fiscale errato");

                //aggiunta della class "danger-color" se il codice fiscale è vuoto
                txtCF.Attributes.Add("class", String.Join(" ", txtCF
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "danger-color" })
                    .Concat(new string[] { "danger-color" })
                    .ToArray()
                ));
            }
        }

        public static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public static bool VerificaCodiceFiscale(string Codice)
        {
            if (Codice != null && Codice != "")
            {
                // Dato un Codice Fiscale verifica il codice di controllo
                // Input: il Codice Fiscale da verificare, 16 caratteri
                // Output: true/false

                Codice = Codice.ToUpper();
                if (Codice.Length != 16)
                    return false; // errore
                else
                {
                    if (Codice.Substring(15, 1) ==
                        CalcolaCodiceControllo(Codice.Substring(0, 15)))
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        private static string CalcolaCodiceControllo(string Codice)
        {
            // Calcola il codice di controllo del Codice Fiscale
            // Input: i primi 15 caratteri del Codice Fiscale
            // Output: il codice di controllo

            int Contatore = 0;
            Codice = Codice.ToUpper();
            if (Codice.Length != 15)
                return "0"; // zero: errore
            else
            {
                for (int i = 0; i < Codice.Length; i++)
                {
                    Contatore += ValoreDelCarattere(Codice.Substring(i, 1), i);
                }
                Contatore %= 26; // si considera il resto
                return Convert.ToChar(Contatore + 65).ToString();
            }
        }

        private static int ValoreDelCarattere(string Carattere, int Posizione)
        {
            int Valore = 0;
            switch (Carattere)
            {
                case "A":
                case "0":
                    if ((Posizione % 2) == 0)
                        Valore = 1;
                    else
                        Valore = 0;
                    break;
                case "B":
                case "1":
                    if ((Posizione % 2) == 0)
                        Valore = 0;
                    else
                        Valore = 1;
                    break;
                case "C":
                case "2":
                    if ((Posizione % 2) == 0)
                        Valore = 5;
                    else
                        Valore = 2;
                    break;
                case "D":
                case "3":
                    if ((Posizione % 2) == 0)
                        Valore = 7;
                    else
                        Valore = 3;
                    break;
                case "E":
                case "4":
                    if ((Posizione % 2) == 0)
                        Valore = 9;
                    else
                        Valore = 4;
                    break;
                case "F":
                case "5":
                    if ((Posizione % 2) == 0)
                        Valore = 13;
                    else
                        Valore = 5;
                    break;
                case "G":
                case "6":
                    if ((Posizione % 2) == 0)
                        Valore = 15;
                    else
                        Valore = 6;
                    break;
                case "H":
                case "7":
                    if ((Posizione % 2) == 0)
                        Valore = 17;
                    else
                        Valore = 7;
                    break;
                case "I":
                case "8":
                    if ((Posizione % 2) == 0)
                        Valore = 19;
                    else
                        Valore = 8;
                    break;
                case "J":
                case "9":
                    if ((Posizione % 2) == 0)
                        Valore = 21;
                    else
                        Valore = 9;
                    break;
                case "K":
                    if ((Posizione % 2) == 0)
                        Valore = 2;
                    else
                        Valore = 10;
                    break;
                case "L":
                    if ((Posizione % 2) == 0)
                        Valore = 4;
                    else
                        Valore = 11;
                    break;
                case "M":
                    if ((Posizione % 2) == 0)
                        Valore = 18;
                    else
                        Valore = 12;
                    break;
                case "N":
                    if ((Posizione % 2) == 0)
                        Valore = 20;
                    else
                        Valore = 13;
                    break;
                case "O":
                    if ((Posizione % 2) == 0)
                        Valore = 11;
                    else
                        Valore = 14;
                    break;
                case "P":
                    if ((Posizione % 2) == 0)
                        Valore = 3;
                    else
                        Valore = 15;
                    break;
                case "Q":
                    if ((Posizione % 2) == 0)
                        Valore = 6;
                    else
                        Valore = 16;
                    break;
                case "R":
                    if ((Posizione % 2) == 0)
                        Valore = 8;
                    else
                        Valore = 17;
                    break;
                case "S":
                    if ((Posizione % 2) == 0)
                        Valore = 12;
                    else
                        Valore = 18;
                    break;
                case "T":
                    if ((Posizione % 2) == 0)
                        Valore = 14;
                    else
                        Valore = 19;
                    break;
                case "U":
                    if ((Posizione % 2) == 0)
                        Valore = 16;
                    else
                        Valore = 20;
                    break;
                case "V":
                    if ((Posizione % 2) == 0)
                        Valore = 10;
                    else
                        Valore = 21;
                    break;
                case "W":
                    if ((Posizione % 2) == 0)
                        Valore = 22;
                    else
                        Valore = 22;
                    break;
                case "X":
                    if ((Posizione % 2) == 0)
                        Valore = 25;
                    else
                        Valore = 23;
                    break;
                case "Y":
                    if ((Posizione % 2) == 0)
                        Valore = 24;
                    else
                        Valore = 24;
                    break;
                case "Z":
                    if ((Posizione % 2) == 0)
                        Valore = 23;
                    else
                        Valore = 25;
                    break;
                default:
                    Valore = 0;
                    break;
            }
            return Valore;
        }

        private bool VerificaPartitaIva(string partitaIva)
        {
            if (partitaIva != null && partitaIva != "")
            {
                partitaIva = partitaIva.Trim();
                try
                {
                    if (partitaIva == "00000000000")
                    {
                        return false;
                    }
                    if (partitaIva.Length == 11)
                    {
                        int tot = 0;
                        int dispari = 0;

                        for (int i = 0; i < 10; i += 2)
                            dispari += int.Parse(partitaIva.Substring(i, 1));

                        for (int i = 1; i < 10; i += 2)
                        {
                            tot = (int.Parse(partitaIva.Substring(i, 1))) * 2;
                            tot = (tot / 10) + (tot % 10);
                            dispari += tot;
                        }

                        int controllo = int.Parse(partitaIva.Substring(10, 1));

                        if (((dispari % 10) == 0 && (controllo == 0))
                           || ((10 - (dispari % 10)) == controllo))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        private Webinar GetWebinar()
        {
            Webinar webinarDaGestire = new Webinar();

            string startTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            string endTime = DateTime.Now.AddDays(7).ToString("yyyy-MM-ddTHH:mm:ss") + "Z"; //la mail viene inviata dai 7 ai 10 giorni prima

            var client = new RestClient("https://api.getgo.com/G2W/rest/v2/organizers/" + organizer_key +"/webinars?fromTime=" + startTime + "&toTime=" + endTime);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            //non dovrebbe più dare il forbidden, ma bisogna vedere se va bene, domani tocca riprovare
            //if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)//da riguardare questa ricorsione => vediamo se domani provando va tutto
            //{
            //    //RefreshTokenGTW();
            //    return GetWebinar();
            //}

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content);

            foreach (Webinar w in myDeserializedClass._embedded.webinars)
            {
                //if (w.webinarID == webinarID)//da chiedere come si può serializzare una cosa del genere
                if(w.webinarKey == webinarKey)
                {
                    webinarDaGestire = w;
                }
            }

            return webinarDaGestire;
        }

        private string CreateRegistrantForWebinar(string webinar_key)
        {
            string note = txtCF.Text.ToUpper();
            string telefono = "";
            string ragioneSociale = "";
            if (txtTelefono.Text != "")
            {
                telefono = txtTelefono.Text;
            }
            if (txtRagioneSociale.Text != "")
            {
                ragioneSociale = txtRagioneSociale.Text;
            }
            if (txtPIVA.Text != "")
            {
                note += ";" + txtPIVA.Text;
            }
            if (radioSi.Checked)
            {
                note += ";" + inputOrdine.Text + ";" + txtNumeroOrdine.Text;
            }
            if (radioNo.Checked)
            {
                note += ";" + inputProvincia.Text;
            }
            var client = new RestClient("https://api.getgo.com/G2W/rest/v2/organizers/" + organizer_key + "/webinars/" + webinar_key + "/registrants");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/x-www-form-urlencoded", "{\n  \"firstName\": " + txtNome.Text + ",\n  \"lastName\": " + "\"" + txtCognome.Text + "\"" + ",\n  \"email\": "
                + "\"" + txtMail.Text + "\", \n \"questionsAndComments\" :" + "\"" + note + "\"," + "\n \"organization\" :" + "\"" + ragioneSociale + "\"," + "\n \"phone\" :" + "\"" + telefono + "\"," + "\n}", ParameterType.RequestBody);
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            IRestResponse response = client.Execute(request);

            return response.Content;

        }

        #region vecchio meteodo per prendere le credenziali da GTW, adesso sono dentro il codice e faccio il refresh ad ogni chiamata, superfluo
        //private void GetTokenGTW()
        //{
        //    string clientId = "a123e14e-4fb7-430f-8395-6314643fd730";
        //    string clientSecret = "SiycgLz9pyWHlPfGKWbUow==";
        //    var oauth2Api = new OAuth2Api(clientId, clientSecret);
        //    string absoluteurl = HttpContext.Current.Request.Url.AbsoluteUri;
        //    string uri = absoluteurl.Substring(0, absoluteurl.Length - (webinarID.Length + 12));
        //    string responseKey = oauth2Api.GetResponseKey(new Uri(uri));
        //    var tokenResponse = oauth2Api.GetAccessTokenResponse(responseKey);

        //    access_token = tokenResponse.access_token;
        //    refresh_token = tokenResponse.refresh_token;
        //    account_key = tokenResponse.account_key;
        //    organizer_key = tokenResponse.organizer_key;
        //}
        #endregion

        private void RefreshTokenGTW()
        {
            string clientId = "dd1d776f-3788-4d61-9bd1-520cd5d9154f";
            string clientSecret = "BdtKdB7iqbNiZitHowB3SQ==";
            var oauth2Api = new OAuth2Api(clientId, clientSecret);
            var tokenResponse = oauth2Api.GetAccessTokenUsingRefreshToken(refresh_token);

            access_token = tokenResponse.access_token;
            refresh_token = tokenResponse.refresh_token;
            account_key = tokenResponse.account_key;
            organizer_key = tokenResponse.organizer_key;
        }

        //private void GetOrganizationsPipeDrive()
        //{
        //    var client = new RestClient("https://api.pipedrive.com/v1/organizations/search?term=" + txtCF.Text.ToUpper() + "&start=0&api_token=" + pipedrive_token);
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Content-Type", "application/json");
        //    IRestResponse response = client.Execute(request);

        //    OrganizationJson myDeserializedClass = JsonConvert.DeserializeObject<OrganizationJson>(response.Content);

        //    if (myDeserializedClass.data.items.Count == 0) //se non ho trovato nessuna organizzazione vuol dire che non ne esiste nessuna e quindi la creo
        //    {
        //       string s = CreateOrganizationPipeDrive();
        //    }
        //    else
        //    {
        //        string PIVATroncata = txtPIVA.Text.TrimStart(new Char[] { '0' });

        //        foreach (Item i in myDeserializedClass.data.items)
        //        {
        //            foreach (string s in i.item.custom_fields)
        //            {
        //                if (s == PIVATroncata)
        //                {
        //                    CreateNoteForOrganizorPipeDrive(i.item.id);
        //                }
        //            }
        //        }
        //    }

        //}

        //private string CreateOrganizationPipeDrive()//da vedere come si aggiungo i custom fields, che dovrebbero essere sempre gli stessi
        //{
        //    var client = new RestClient("https://api.pipedrive.com/v1/organizations?api_token=" + pipedrive_token);
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");
        //    request.AddParameter("application/x-www-form-urlencoded", "{\n  \"name\": \"" + txtNome.Text + " " + txtCognome.Text +  "\" }", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);

        //    ResponseCreationOrganization resp = JsonConvert.DeserializeObject<ResponseCreationOrganization>(response.Content);

        //    CreateNoteForOrganizorPipeDrive(resp.data.id);

        //    return response.Content;
        //}

        //private void CreateNoteForOrganizorPipeDrive(int org_id)//da testare
        //{
        //    string note = "Compilazione Webinar " + webinar.subject + " => email : " + txtMail.Text + ", " + "CF : " + txtCF.Text.ToUpper();
        //    if (txtTelefono.Text != "")
        //    {
        //        note += ", telefono : " + txtTelefono.Text;
        //    }
        //    if (txtRagioneSociale.Text != "")
        //    {
        //        note += " , ragione sociale : " + txtRagioneSociale.Text;
        //    }
        //    if (txtPIVA.Text != "")
        //    {
        //        note += " , Partita IVA : " + txtPIVA.Text;
        //    }
        //    if (radioSi.Checked)
        //    {
        //        note += " , Ordine di appartenenza : " + inputOrdine.Text + " , numero :" + txtNumeroOrdine.Text;
        //    }
        //    if (radioNo.Checked)
        //    {
        //        note += ", Provincia : " + inputProvincia.Text;
        //    }

        //    var client = new RestClient("https://api.pipedrive.com/v1/notes?api_token=" + pipedrive_token);
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Accept", "application/json");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddParameter("application/application/x-www-form-urlencoded", "{\n \"content\": \"" + note + "\",\n \"org_id\": "+ org_id + "\n}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //}

        #region non creo piu un activity ma solo una nota, poi verrà creata quando risponderò al survey
        //private string CreateActivityForUserPipeDrive(int org_id)
        //{
        //    string note = "email : " + txtMail.Text + ", " + "CF : " + txtCF.Text.ToUpper();
        //    if (txtTelefono.Text != "")
        //    {
        //        note += ", telefono : " + txtTelefono.Text;
        //    }
        //    if (txtRagioneSociale.Text != "")
        //    {
        //        note += " , ragione sociale : " + txtRagioneSociale.Text;
        //    }
        //    if (txtPIVA.Text != "")
        //    {
        //        note += " , Partita IVA : " + txtPIVA.Text;
        //    }
        //    if (radioSi.Checked)
        //    {
        //        note += " , Ordine di appartenenza : " + inputOrdine.Text + " , numero :" + txtNumeroOrdine.Text;
        //    }
        //    if (radioNo.Checked)
        //    {
        //        note += ", Provincia : " +  inputProvincia.Text;
        //    }
        //    //aggiungere anche l'if che permette di capire se è stata inserita la provicia
        //    //voglio capire se mettere anche l'ordine di appartenenza, anche se ha senso metterlo
        //    var client = new RestClient("https://api.pipedrive.com/v1/activities?api_token=" + pipedrive_token);
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");
        //    request.AddParameter("application/x-www-form-urlencoded", "{\n  \"due-date\": \"" + DateTime.Now.ToString() + "\",\n \"org_id\": \"" + org_id + "\",\n \"note\":\"" + note + "\",\n \"subject\":\"compilazione webinar " + webinar.subject +  "\" }", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    return response.Content;
        //}
        #endregion

    }
}
