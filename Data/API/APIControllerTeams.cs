using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ITC2Wedstrijd.Data.API.ApiClass;

namespace ITC2Wedstrijd.Data.API
{
    public class APIControllerTeams
    {
        public static async Task<List<Team>> GetTeams()
        {
            // Get teams from API
            string url = "https://www.thesportsdb.com/api/v1/json/3/search_all_teams.php?s=Soccer&c=Belgium";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            if (response == null)
            {
                return new List<Team>();
            }
            var data = JsonConvert.DeserializeObject<TeamResponse>(response);
            if (data != null)
            {
                return data.Teams;
            }
            return new List<Team>();
        }
    }
}
