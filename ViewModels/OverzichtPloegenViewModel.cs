using ITC2Wedstrijd.Data.IRepository;
using ITC2Wedstrijd.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.ViewModels
{
    public partial class OverzichtPloegenViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Club> clubs;

        private IOverzichtPloegenRepository _overzichtPloegenRepository;

        public OverzichtPloegenViewModel(OverzichtPloegenRepository overzichtPloegenRepository)
        {
            _overzichtPloegenRepository = overzichtPloegenRepository;
            OphalenClubsMetPloegen();

        }

        [RelayCommand]
        public void OphalenClubsMetPloegen()
        {
            Clubs = new ObservableCollection<Club>(_overzichtPloegenRepository.PloegenPerClubOphalen());
        }
    }
}
