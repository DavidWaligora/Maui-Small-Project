using ITC2Wedstrijd.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.Data.Repository
{
    public class WedstrijdenRepository : BaseRepository, IWedstrijdenRepository
    {
        public bool ToevoegenWedstrijd(Wedstrijd wedstrijd)
        {
            string sql = @"INSERT INTO Wedstrijd 
                    (ploeg1id, ploeg2id, UitslagPloeg1, UitslagPloeg2, Datum) 
                    VALUES (@Ploeg1Id, @Ploeg2Id, @UitslagPloeg1, @UitslagPloeg2, @Datum)";
            using IDbConnection db = new SqlConnection(ConnectionString);
            var affectedRows = db.Execute(sql, new
            {
                Ploeg1Id = wedstrijd.Ploeg1.Id,
                Ploeg2Id = wedstrijd.Ploeg2.Id,
                UitslagPloeg1 = wedstrijd.UitslagPloeg1,
                UitslagPloeg2 = wedstrijd.UitslagPloeg2,
                Datum = wedstrijd.Datum
            });

            return affectedRows == 1;
        }

        public bool VerwijderenWedstrijd(int id)
        {
            string sql = @"DELETE FROM Wedstrijd WHERE Id = @id";
            using IDbConnection db = new SqlConnection(ConnectionString);
            var affectedRows = db.Execute(sql, new { id });
            return affectedRows == 1;
        }

        public IEnumerable<Wedstrijd> WedstrijdenOphalen()
        {
            string sql = @"SELECT * FROM Wedstrijd W 
                                    INNER JOIN Ploeg P1 ON W.ploeg1id = P1.id 
                                    INNER JOIN Ploeg P2 ON W.ploeg2id = P2.id";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var Wedstrijden = db.Query<Wedstrijd, Ploeg, Ploeg, Wedstrijd>(sql,
                    (wedstrijd, ploeg1, ploeg2) =>
                    {
                        wedstrijd.Ploeg1 = ploeg1;
                        wedstrijd.Ploeg2 = ploeg2;
                        return wedstrijd;
                    }, splitOn: "Id"
                    ).ToList();
                return Wedstrijden;
            }
        }

        public bool WijzigenWedstrijd(Wedstrijd wedstrijd)
        {
            string sql = @"UPDATE Wedstrijd
                   SET ploeg1id = @Ploeg1Id, ploeg2id = @Ploeg2Id, UitslagPloeg1 = @UitslagPloeg1, UitslagPloeg2 = @UitslagPloeg2, Datum = @Datum 
                   WHERE id = @Id";
            using IDbConnection db = new SqlConnection(ConnectionString);
            var affectedRows = db.Execute(sql, new
            {
                Ploeg1Id = wedstrijd.Ploeg1.Id,
                Ploeg2Id = wedstrijd.Ploeg2.Id,
                UitslagPloeg1 = wedstrijd.UitslagPloeg1,
                UitslagPloeg2 = wedstrijd.UitslagPloeg2,
                Datum = wedstrijd.Datum,
                Id = wedstrijd.Id
            });
            return affectedRows == 1;
        }
    }
}
