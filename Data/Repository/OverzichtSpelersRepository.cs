using ITC2Wedstrijd.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.Data.Repository
{
    public class OverzichtSpelersRepository: BaseRepository, IOverzichtSpelersRepository
    {
        public IEnumerable<Ploeg> PloegenOphalenMetSpelers()
        {
            var sql = @"SELECT P.*, '' AS SplitCol, S.*
                FROM Ploeg P
                JOIN SpelerPloeg SP ON SP.ploegId = P.id
                JOIN Speler S on SP.spelerID = s.id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var ploegen = db.Query<Ploeg, Speler, Ploeg>(
                    sql,
                    (ploeg, speler) =>
                    {
                        ploeg.Spelers = [speler];
                        return ploeg;
                    }, splitOn: "SplitCol");

                return SorteerPloegen(ploegen);
            }
        }

        private static IEnumerable<Ploeg> SorteerPloegen(IEnumerable<Ploeg> ploegen)
        {
            var gesorteerd = ploegen.GroupBy(p => p.Naam);

            List<Ploeg> ploegMetSpelers = new List<Ploeg>();

            foreach (var g in gesorteerd)
            {
                var ploeg = g.First();
                List<Speler> ploegSpelers = new List<Speler>();

                foreach (var p in g)
                {
                    ploegSpelers.Add(p.Spelers.First());
                }

                ploeg.Spelers = ploegSpelers.OrderBy(s => s.Naam).ToList();
                ploegMetSpelers.Add(ploeg);
            }

            return ploegMetSpelers;
        }
    }
}
