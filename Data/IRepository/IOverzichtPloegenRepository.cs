using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.Data.IRepository
{
    public interface IOverzichtPloegenRepository
    {
        public IEnumerable<Club> PloegenPerClubOphalen();
    }
}
