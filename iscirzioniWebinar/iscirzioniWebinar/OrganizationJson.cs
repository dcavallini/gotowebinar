using System;
using System.Collections.Generic;

namespace iscirzioniWebinar
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Owner
    {
        public int id { get; set; }
    }

    public class Item2
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public object address { get; set; }
        public int visible_to { get; set; }
        public Owner owner { get; set; }
        public List<string> custom_fields { get; set; }
        public List<object> notes { get; set; }
    }

    public class Item
    {
        public double result_score { get; set; }
        public Item2 item { get; set; }
    }

    public class Datas
    {
        public List<Item> items { get; set; }
    }

    public class Pagination
    {
        public int start { get; set; }
        public int limit { get; set; }
        public bool more_items_in_collection { get; set; }
    }

    public class AdditionalData
    {
        public Pagination pagination { get; set; }
    }

    public class OrganizationJson
    {
        public bool success { get; set; }
        public Datas data { get; set; }
        public AdditionalData additional_data { get; set; }
    }

}
