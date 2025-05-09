using ITC2Wedstrijd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.ViewModels
{
    public partial class OverzichtWedstrijdenViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Wedstrijd> wedstrijden;

        [ObservableProperty]
        public ObservableCollection<Ploeg> ploegen;

        private IWedstrijdenRepository _wedstrijdenRepository;
        private IPloegRepository _ploegRepository;

        public OverzichtWedstrijdenViewModel(WedstrijdenRepository wedstrijdenRepository, PloegRepository ploegRepository)
        {
            _wedstrijdenRepository = wedstrijdenRepository;
            _ploegRepository = ploegRepository;
            OphalenUitslagen();
        }

        [RelayCommand]
        public void OphalenUitslagen()
        {
            Wedstrijden = new ObservableCollection<Wedstrijd>(_wedstrijdenRepository.WedstrijdenOphalen());
            Ploegen = new ObservableCollection<Ploeg>(_ploegRepository.PloegOphalen());


            foreach (var wedstrijd in wedstrijden)
            {
                var ploeg1 = Ploegen.Where(x => x.Id == wedstrijd.Ploeg1.Id).FirstOrDefault();
                var ploeg2 = Ploegen.Where(x => x.Id == wedstrijd.Ploeg2.Id).FirstOrDefault();

                if (wedstrijd.Ploeg1 != null || wedstrijd.Ploeg2 != null)
                {
                    if (wedstrijd.UitslagPloeg1 > wedstrijd.UitslagPloeg2)
                    {
                        ploeg1.AantalGewonnen++;
                        ploeg2.AantalVerloren++;

                    }
                    else if (wedstrijd.UitslagPloeg1 < wedstrijd.UitslagPloeg2)
                    {
                        ploeg1.AantalVerloren++;
                        ploeg2.AantalGewonnen++;
                    }
                    else
                    {
                        ploeg1.AantalGelijk++;
                        ploeg2.AantalGelijk++;
                    }
                }
            }
        }
    }
}
