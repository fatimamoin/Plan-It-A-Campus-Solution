using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;



namespace PlanIt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Events_has_Member
    {
        public int idEvents_has_Member { get; set; }
        public int Club_member_idClub_members { get; set; }
        public int Events_idEvents { get; set; }
        public string Why { get; set; }
    
        public virtual Club_member Club_member { get; set; }
        public virtual Event Event { get; set; }

        public static int seats(int remaining)
        {
            using (var client = new HttpClient())
            {
                var conf = ConfigurationManager.AppSettings;
                client.BaseAddress = new Uri(conf["ClientID"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                string requestQuery = string.Format(
                    "api/TaxCalculator/RemainingSeats?remaining={0}",
                    remaining);
                HttpResponseMessage response = client.GetAsync(requestQuery).Result;
                if (response.IsSuccessStatusCode)
                {
                    var s = response.Content.ReadAsStringAsync().Result;
                    var seats = System.Convert.ToInt32(s);
                    return seats;
                }
            }
            return -1;
        }
    }
}
