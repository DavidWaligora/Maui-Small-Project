
namespace ITC2Wedstrijd.ViewModels
{
    public partial class ClubViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Club> club;

        private IClubRepository _clubRepository;

        [ObservableProperty]
        public Club selectedClub;

        public ClubViewModel(ClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
            RefreshClub();
            SelectedClub = new Club();
            Title = "Club";
        }

        [ObservableProperty]
        public string actieLabel = "Nieuwe club toevoegen";


        [ObservableProperty]
        public string foutMelding;

        partial void OnSelectedClubChanged(Club value)
        {
            if (value.Id == 0)
            {
                ActieLabel = "Nieuwe club toevoegen";
            }
            else
            {
                ActieLabel = "Club wijzigen";
            }
        }


        private void RefreshClub()
        {
            Club = new ObservableCollection<Club>(_clubRepository.ClubOphalen());
        }

        private bool ValidatieClub()
        {
            if (SelectedClub.Naam == null || SelectedClub.Naam == "")
            {
                FoutMelding = "Naam mag niet leeg zijn.";
                return false;
            }
            foutMelding = string.Empty;
            return true;
        }

        [RelayCommand]
        public void Toevoegen()
        {
            var result = _clubRepository.ToevoegenClub(SelectedClub);

            if (!ValidatieClub()) return;

            if (result)
            {
                RefreshClub();
                SelectedClub = new Club();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het toevoegen van de club", "OK");
            }
        }


        [RelayCommand]
        public void Wijzigen()
        {
            var result = _clubRepository.WijzigenClub(selectedClub);

            if (!ValidatieClub()) return;

            if (result)
            {
                RefreshClub();
                SelectedClub = new Club();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het wijzigen van de club", "OK");
            }
        }


        [RelayCommand]
        public void Verwijderen()
        {
            var result = _clubRepository.VerwijderenClub(SelectedClub.Id);

            if (result)
            {
                RefreshClub();
                SelectedClub = new Club();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het verwijderen van de club", "OK");
            }
        }

        [RelayCommand]
        public void Deselecteren()
        {
            SelectedClub = new Club();
        }
    }
}