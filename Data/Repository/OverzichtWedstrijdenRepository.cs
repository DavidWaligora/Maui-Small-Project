using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.Data.Repository
{
    public class OverzichtWedstrijdenRepository : BaseRepository, IOverzichtWedstrijdenRepository
    {
        public IEnumerable<Wedstrijd> WedstrijdenOphalen()
        {
            string sql = @"SELECT Wedstrijd.*, Ploeg1.*, Ploeg2.*
                       FROM Wedstrijd  
                       INNER JOIN Ploeg Ploeg1 ON Wedstrijd.ploeg1id = Ploeg1.id  
                       INNER JOIN Ploeg Ploeg2 ON Wedstrijd.ploeg2id = Ploeg2.id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var wedstrijden = db.Query<Wedstrijd, Ploeg, Ploeg, Wedstrijd>(
                    sql,
                    (wedstrijd, ploeg1, ploeg2) =>
                    {
                        wedstrijd.Ploeg1 = ploeg1;
                        wedstrijd.Ploeg2 = ploeg2;
                        return wedstrijd;
                    },
                    splitOn: "Ploeg1Id, Ploeg2Id"
                ).ToList();

                return wedstrijden;
            }
        }
    }
}

