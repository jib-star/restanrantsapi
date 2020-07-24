using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurants.Models
{
    public class RestaurantsModel
    {
        public string error_message { get; set; }
        public List<data> results { get; set; } = new List<data>();
        public string status { get; set; }

        public class data
        {
            public string business_status { get; set; }
            public string formatted_address { get; set; }
            public string icon { get; set; }
            public string name { get; set; }
            public opening_hours opening_hours { get; set; } = new opening_hours();
            public string user_ratings_total { get; set; }
        }

        public class opening_hours
        {
            public bool open_now { get; set; }
        }
    }
}