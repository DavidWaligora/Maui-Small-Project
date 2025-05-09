using ITC2Wedstrijd.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.ViewModels
{
    public partial class WedstrijdenViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Wedstrijd> wedstrijden;

        [ObservableProperty]
        public ObservableCollection<Ploeg> ploegen;

        [ObservableProperty]
        public Wedstrijd selectedWedstrijd;

        private WedstrijdenRepository _wedstrijdenRepository;
        private PloegRepository _ploegRepository;

        [ObservableProperty]
        public string actieLabel = "Nieuwe Wedstrijd toevoegen";

        [ObservableProperty]
        public string foutMelding;

        public WedstrijdenViewModel(WedstrijdenRepository wedstrijdenRepository, PloegRepository ploegRepository)
        {
            Title = "Wedstrijden";

            _wedstrijdenRepository = wedstrijdenRepository;
            _ploegRepository = ploegRepository;

            RefreshWedstrijden();
            OphalenPloegen();


            SelectedWedstrijd = new Wedstrijd();
            SelectedWedstrijd.Datum = DateTime.Now;
        }

        partial void OnSelectedWedstrijdChanged(Wedstrijd value)
        {
            if (value.Id == 0)
            {
                ActieLabel = "Nieuwe wedstrijd toevoegen";
            }
            else
            {
                ActieLabel = "Speler wijzigen";
            }
        }
        private void OphalenPloegen()
        {
            Ploegen = new ObservableCollection<Ploeg>(_ploegRepository.PloegOphalen());
        }
        private void RefreshWedstrijden()
        {
            Wedstrijden = new ObservableCollection<Wedstrijd>(_wedstrijdenRepository.WedstrijdenOphalen());
        }

        private bool ValidatieWedstrijd()
        {
            List<string> fouten = new List<string>();

            if (SelectedWedstrijd?.Ploeg1 == null)
            {
                fouten.Add("Ploeg1 mag niet leeg zijn.");
            }

            if (SelectedWedstrijd?.Ploeg2 == null)
            {
                fouten.Add("Ploeg2 mag niet leeg zijn.");
            }

            if (SelectedWedstrijd.Ploeg1 == SelectedWedstrijd.Ploeg2)
            {
                fouten.Add("Ploeg 1 moet verschillend zijn van ploeg 2.");
            }
            if (SelectedWedstrijd.Datum > DateTime.Now)
            {
                fouten.Add("Datum mag niet in de toekomst liggen.");
            }
            if(SelectedWedstrijd.UitslagPloeg1 < 0 || SelectedWedstrijd.UitslagPloeg2 < 0)
            {
                fouten.Add("Score kan niet negatief zijn");
            }

            FoutMelding = string.Join("\n", fouten);

            return !fouten.Any();
        }

        [RelayCommand]
        private void Toevoegen()
        {
            if (!ValidatieWedstrijd()) return;

            IsBusy = true;

            var result = _wedstrijdenRepository.ToevoegenWedstrijd(SelectedWedstrijd);
            if (result)
            {
                RefreshWedstrijden();
                SelectedWedstrijd = new Wedstrijd();
                SelectedWedstrijd.Datum = DateTime.Now;
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het toevoegen", "OK");
            }
            IsBusy = false;
        }
        [RelayCommand]
        private void Wijzigen()
        {
            if (!ValidatieWedstrijd()) return;

            IsBusy = true;
            var result = _wedstrijdenRepository.WijzigenWedstrijd(SelectedWedstrijd);
            if (result)
            {
                RefreshWedstrijden();
                SelectedWedstrijd = new Wedstrijd();
                SelectedWedstrijd.Datum = DateTime.Now;
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het wijzigen", "OK");
            }
            IsBusy = false;
        }
        [RelayCommand]
        private void Verwijderen()
        {
            IsBusy = true;
            var result = _wedstrijdenRepository.VerwijderenWedstrijd(SelectedWedstrijd.Id);
            if (result)
            {
                RefreshWedstrijden();
                SelectedWedstrijd = new Wedstrijd();
                SelectedWedstrijd.Datum = DateTime.Now;
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het verwijderen", "OK");
            }
            IsBusy = false;
        }
        [RelayCommand]
        private void Deselecteren()
        {
            IsBusy = true;
            SelectedWedstrijd = new Wedstrijd();
            IsBusy = false;
        }
    }
}
