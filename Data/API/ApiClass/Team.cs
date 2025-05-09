using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.Models
{
    public class Team
    {
        [DataMember]
        public int idTeam { get; set; }
        [DataMember] 
        public string strSport { get; set; }
        [DataMember]
        public string strTeam { get; set; }
        [DataMember] 
        public string strCountry { get; set; }
        [DataMember]
        public string strWebsite { get; set; }
        [DataMember]
        public string strBadge { get; set; }
    }
}
