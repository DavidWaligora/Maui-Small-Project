namespace ITC2Wedstrijd.ViewModels
{
    public partial class PloegViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Ploeg> ploeg;

        [ObservableProperty]
        public ObservableCollection<Sport> sport;

        [ObservableProperty]
        public ObservableCollection<Club> club; // Renamed to avoid conflict with type name

        [ObservableProperty]
        public ObservableCollection<Categorie> categorie;

        [ObservableProperty]
        public Ploeg selectedPloeg;

        [ObservableProperty]
        public string actieLabel = "Nieuwe ploeg toevoegen";

        [ObservableProperty]
        public string foutMelding;

        private PloegRepository _ploegRepository;
        private ClubRepository _clubRepository;
        private SportRepository _sportRepository;
        private CategorieRepository _categorieRepository;

        public PloegViewModel(PloegRepository ploegRepository, ClubRepository clubRepository, SportRepository sportRepository, CategorieRepository categorieRepository)
        {
            _ploegRepository = ploegRepository;
            _clubRepository = clubRepository;
            _sportRepository = sportRepository;
            _categorieRepository = categorieRepository;
            RefreshPloeg();
            Club = new ObservableCollection<Club>(_clubRepository.ClubOphalen()); // Updated to use renamed property
            Sport = new ObservableCollection<Sport>(_sportRepository.SportOphalen());
            Categorie = new ObservableCollection<Categorie>(_categorieRepository.CategorieOphalen());

            SelectedPloeg = new Ploeg();
            Title = "Ploeg";
        }

        private bool ValidatiePloeg()
        {
            List<string> fouten = new List<string>();
            if (string.IsNullOrWhiteSpace(SelectedPloeg.Naam))
            {
                fouten.Add("Naam mag niet leeg zijn.");
            }
            if (SelectedPloeg.Sport == null)
            {
                fouten.Add("Sport mag niet leeg zijn.");
            }
            if (SelectedPloeg.Club == null)
            {
                fouten.Add("Club mag niet leeg zijn.");
            }
            if (SelectedPloeg.Categorie == null)
            {
                fouten.Add("Categorie mag niet leeg zijn.");
            }

            FoutMelding = string.Join("\n", fouten);

            return !fouten.Any();
        }

        [RelayCommand]
        public void Toevoegen()
        {
            if (!ValidatiePloeg()) return;
            var result = _ploegRepository.ToevoegenPloeg(SelectedPloeg);


            if (result)
            {
                RefreshPloeg();
                SelectedPloeg = new Ploeg();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het toevoegen van de ploeg", "OK");
            }
        }

        [RelayCommand]
        public void Wijzigen()
        {
            var result = _ploegRepository.WijzigenPloeg(SelectedPloeg);

            if (!ValidatiePloeg()) return;

            if (result)
            {
                RefreshPloeg();
                SelectedPloeg = new Ploeg();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het wijzigen van de ploeg", "OK");
            }
        }

        [RelayCommand]
        public void Verwijderen()
        {
            var result = _ploegRepository.VerwijderenPloeg(SelectedPloeg.Id);

            if (result)
            {
                RefreshPloeg();
                SelectedPloeg = new Ploeg();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het verwijderen van de ploeg", "OK");
            }
        }

        [RelayCommand]
        public void Deselecteren()
        {
            SelectedPloeg = new Ploeg();
        }

        [RelayCommand]
        public async Task GoToToewijzen()
        {
            if (SelectedPloeg.Id == 0)
            {
                await Shell.Current.DisplayAlert("Fout", "Je moet eerst een ploeg selecteren!", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(ToewijzenPage), true, new Dictionary<string, object>
                    {
                        {"Ploeg", SelectedPloeg }
                    });
            }
        }

        partial void OnSelectedPloegChanged(Ploeg value)
        {
            if (value == null) return;

            if (value.Id == 0)
            {
                ActieLabel = "Nieuwe ploeg toevoegen";
            }
            else
            {
                ActieLabel = "Ploeg wijzigen";
            }
        }

        private void RefreshPloeg()
        {
            Ploeg = new ObservableCollection<Ploeg>(_ploegRepository.PloegOphalen());
        }
    }
}