using ITC2Wedstrijd.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.ViewModels
{
    public partial class OverzichtSpelersViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Ploeg> ploegen;

        private OverzichtSpelersRepository _overzichtSpelersRepository;

        public OverzichtSpelersViewModel(OverzichtSpelersRepository overzichtSpelersRepository)
        {
            _overzichtSpelersRepository = overzichtSpelersRepository;
            OphalenPloegenMetSpelers();

        }

        [RelayCommand]
        public void OphalenPloegenMetSpelers()
        {
            Ploegen = new ObservableCollection<Ploeg>(_overzichtSpelersRepository.PloegenOphalenMetSpelers());
        }

    }
}
