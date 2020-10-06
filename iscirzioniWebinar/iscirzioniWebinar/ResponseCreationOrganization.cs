using System;
namespace iscirzioniWebinar
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class Id
    {
        public int id { get; set; }
    }

    public class ResponseCreationOrganization
    {
        public bool success { get; set; }
        public Id data { get; set; }
    }


}
