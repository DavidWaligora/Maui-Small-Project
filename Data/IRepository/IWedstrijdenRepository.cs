using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.Data.IRepository
{
    public interface IWedstrijdenRepository
    {
        public IEnumerable<Wedstrijd> WedstrijdenOphalen();
        bool ToevoegenWedstrijd(Wedstrijd wedstrijd);

        bool VerwijderenWedstrijd(int id);
        bool WijzigenWedstrijd(Wedstrijd wedstrijd);
    }
}
