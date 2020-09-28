using System;
namespace iscirzioniWebinar
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class Data
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public string name { get; set; }
        public int open_deals_count { get; set; }
        public int related_open_deals_count { get; set; }
        public int closed_deals_count { get; set; }
        public int related_closed_deals_count { get; set; }
        public int email_messages_count { get; set; }
        public int people_count { get; set; }
        public int activities_count { get; set; }
        public int done_activities_count { get; set; }
        public int undone_activities_count { get; set; }
        public int files_count { get; set; }
        public int notes_count { get; set; }
        public int followers_count { get; set; }
        public int won_deals_count { get; set; }
        public int related_won_deals_count { get; set; }
        public int lost_deals_count { get; set; }
        public int related_lost_deals_count { get; set; }
        public bool active_flag { get; set; }
        public object category_id { get; set; }
        public object picture_id { get; set; }
        public object country_code { get; set; }
        public string first_char { get; set; }
        public string update_time { get; set; }
        public string add_time { get; set; }
        public string visible_to { get; set; }
        public object next_activity_date { get; set; }
        public object next_activity_time { get; set; }
        public object next_activity_id { get; set; }
        public object last_activity_id { get; set; }
        public object last_activity_date { get; set; }
        public object label { get; set; }
        public object address { get; set; }
        public object address_subpremise { get; set; }
        public object address_street_number { get; set; }
        public object address_route { get; set; }
        public object address_sublocality { get; set; }
        public object address_locality { get; set; }
        public object address_admin_area_level_1 { get; set; }
        public object address_admin_area_level_2 { get; set; }
        public object address_country { get; set; }
        public object address_postal_code { get; set; }
        public object address_formatted_address { get; set; }
        public string cc_email { get; set; }
        public string owner_name { get; set; }
        public bool edit_name { get; set; }
    }


    public class Organization
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }

}
