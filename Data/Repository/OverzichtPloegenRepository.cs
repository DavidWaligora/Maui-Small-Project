using ITC2Wedstrijd.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.Data.Repository
{
    public class OverzichtPloegenRepository : BaseRepository, IOverzichtPloegenRepository
    {
        public IEnumerable<Club> PloegenPerClubOphalen()
        {
            string sql = @"SELECT C.*, P.*
            FROM Club c
            INNER JOIN Ploeg p ON c.Id = p.clubId
            ORDER BY c.naam, p.id";

            using IDbConnection db = new SqlConnection(ConnectionString);
            var ploegen = db.Query<Club, Ploeg, Club>(
                sql, (club, ploeg) =>
                {
                    ploeg.Club = club;
                    club.Ploegen = new List<Ploeg>() { ploeg };
                    return club;
                });

            return SorteerClubs(ploegen);
        }

        private static ICollection<Club> SorteerClubs(IEnumerable<Club> clubs)
        {
            var gesorteerd = clubs.GroupBy(c => c.Id);

            List<Club> clubsMetPloegen = new List<Club>();
            foreach (var g in gesorteerd)
            {
                var club = g.First();
                List<Ploeg> allePloegen = new List<Ploeg>();
                foreach (var c in g)
                {
                    allePloegen.Add(c.Ploegen.First());
                }
                club.Ploegen = allePloegen;
                clubsMetPloegen.Add(club);
            }
            return clubsMetPloegen;
        }
    }
}
