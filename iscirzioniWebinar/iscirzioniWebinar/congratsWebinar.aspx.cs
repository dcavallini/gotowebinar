using System;
using System.Web;
using System.Web.UI;

namespace iscirzioniWebinar
{

    public partial class congratsWebinar : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs args)
        {
            //Response.Redirect("congratsWebinar.aspx?subject=" + webinar.subject + "&start=" + webinar.times[0].startTime + "&end=" + webinar.times[0].endTime +
                       //"&timezone=" + webinar.timeZone + "&participation=" + webinar.registrationUrl);
            string subject = Request.QueryString["subject"];
            string start = Request.QueryString["start"];
            string end = Request.QueryString["end"];
            //string partecipation = Request.QueryString["participation"];

            titoloWebinar.InnerText = subject;
            dataEOra.InnerText = start + " - " + end;
            //webinarLink.HRef = partecipation;
        }
    }
}
