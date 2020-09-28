using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace iscirzioniWebinar
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Time
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }

    public class Webinar
    {
        public string webinarKey { get; set; }
        public string webinarID { get; set; }
        public string organizerKey { get; set; }
        public string accountKey { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public List<Time> times { get; set; }
        public string timeZone { get; set; }
        public string locale { get; set; }
        public string approvalType { get; set; }
        public string registrationUrl { get; set; }
        public bool impromptu { get; set; }
        public bool isPasswordProtected { get; set; }
        public string recurrenceType { get; set; }
        public string experienceType { get; set; }
    }

    public class Embedded
    {
        public List<Webinar> webinars { get; set; }
    }

    public class Root
    {
        public Embedded _embedded { get; set; }
    }


}
