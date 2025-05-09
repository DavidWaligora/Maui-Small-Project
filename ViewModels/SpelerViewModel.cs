

namespace ITC2Wedstrijd.ViewModels
{
    public partial class SpelerViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Speler> speler;

        private SpelerRepository _spelerRepository;

        [ObservableProperty]
        private Array geslacht;

        [ObservableProperty]
        public Speler selectedSpeler;
        public SpelerViewModel(SpelerRepository spelerRepository)
        {
            _spelerRepository = spelerRepository;
            RefreshCategorie();
            Geslacht = typeof(Geslacht).GetEnumValues();
            selectedSpeler = new Speler();
            Title = "Speler";
        }

        [ObservableProperty]
        public string actieLabel = "Nieuwe speler toevoegen";

        [ObservableProperty]
        public string foutMelding;

        partial void OnSelectedSpelerChanged(Speler value)
        {
            if (value.Id == 0)
            {
                ActieLabel = "Nieuwe speler toevoegen";
            }
            else
            {
                ActieLabel = "Speler wijzigen";
            }
        }


        private void RefreshCategorie()
        {
            Speler = new ObservableCollection<Speler>(_spelerRepository.SpelerOphalen());
        }

        private bool ValidatieSpeler()
        {
            List<string> fouten = new List<string>();
            if (string.IsNullOrWhiteSpace(SelectedSpeler.Naam))
            {
                fouten.Add("Naam mag niet leeg zijn.");
            }
            if (string.IsNullOrWhiteSpace(SelectedSpeler.Voornaam))
            {
                fouten.Add("Voornaam mag niet leeg zijn.");
            }
            if (SelectedSpeler.Straat == null)
            {
                fouten.Add("Straat mag niet leeg zijn.");
            }
            if (SelectedSpeler.Huisnummer == null)
            {
                fouten.Add("Huisnummer mag niet leeg zijn.");
            }
            if (SelectedSpeler.Postcode == null)
            {
                fouten.Add("Postcode mag niet leeg zijn.");
            }
            if (SelectedSpeler.Gemeente == null)
            {
                fouten.Add("Gemeente mag niet leeg zijn.");
            }
            if (SelectedSpeler.Land == null)
            {
                fouten.Add("Land mag niet leeg zijn.");
            }
            if (SelectedSpeler.Geboortedatum == null)
            {
                fouten.Add("Geboortedatum mag niet leeg zijn.");
            }
            if (SelectedSpeler.Geboortedatum > DateTime.Now)
            {
                fouten.Add("De geboortedatum mag niet in de toekomst liggen.");
            }


            FoutMelding = string.Join("\n", fouten);

            return !fouten.Any();
        }

        [RelayCommand]
        public void Toevoegen()
        {
            var result = _spelerRepository.ToevoegenSpeler(SelectedSpeler);

            if (!ValidatieSpeler()) return;

            if (result)
            {
                RefreshCategorie();
                SelectedSpeler = new Speler();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het toevoegen van de speler", "OK");
            }
        }


        [RelayCommand]
        public void Wijzigen()
        {
            var result = _spelerRepository.WijzigenSpeler(SelectedSpeler);

            if (!ValidatieSpeler()) return;

            if (result)
            {
                RefreshCategorie();
                SelectedSpeler = new Speler();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het wijzigen van de speler", "OK");
            }
        }


        [RelayCommand]
        public void Verwijderen()
        {
            var result = _spelerRepository.VerwijderenSpeler(SelectedSpeler.Id);

            if (result)
            {
                RefreshCategorie();
                SelectedSpeler = new Speler();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het verwijderen van de speler", "OK");
            }
        }

        [RelayCommand]
        public void Deselecteren()
        {
            SelectedSpeler = new Speler();
        }
    }
}