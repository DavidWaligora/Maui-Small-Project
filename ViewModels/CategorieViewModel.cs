﻿


using System.Globalization;
using System.Numerics;

namespace ITC2Wedstrijd.ViewModels
{
    public partial class CategorieViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Categorie> categorie;

        private ICategorieRepository _categorieRepository;

        [ObservableProperty]
        private Array categorieType;

        [ObservableProperty]
        public Categorie selectedCategorie;
        public CategorieViewModel(CategorieRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
            RefreshCategorie();
            CategorieType = typeof(CategorieType).GetEnumValues();
            selectedCategorie = new Categorie();
            Title = "Categorie";
        }

        [ObservableProperty]
        public string actieLabel = "Nieuwe categorie toevoegen";


        [ObservableProperty]
        public string foutMelding;

        partial void OnSelectedCategorieChanged(Categorie value)
        {
            if (value.Id == 0)
            {
                ActieLabel = "Nieuwe categorie toevoegen";
            }
            else
            {
                ActieLabel = "Categorie wijzigen";
            }
        }


        private void RefreshCategorie()
        {
            Categorie = new ObservableCollection<Categorie>(_categorieRepository.CategorieOphalen());
        }

        private bool ValidatieCategorie()
        {

            List<string> fouten = new List<string>();

            if (string.IsNullOrWhiteSpace(SelectedCategorie.Naam))
            {
                fouten.Add("Naam is verplicht.");
            }
            if (SelectedCategorie.MinLeeftijd == 0)
            {
                fouten.Add("Minimale leeftijd is verplicht.");
            }
            if (SelectedCategorie.MaxLeeftijd == 0)
            {
                fouten.Add("Maximale leeftijd is verplicht.");
            }
            if (SelectedCategorie.MinLeeftijd > SelectedCategorie.MaxLeeftijd)
            {
                fouten.Add("Minimale leeftijd mag niet groter zijn dan maximale leeftijd.");
            }
            if (SelectedCategorie.MinLeeftijd < 0 || SelectedCategorie.MaxLeeftijd < 0)
            {
                fouten.Add("De leeftijd kan niet negatief zijn");
            }

            FoutMelding = string.Join("\n", fouten);

            return !fouten.Any();
        }

        [RelayCommand]
        public void Toevoegen()
        {

            var result = _categorieRepository.ToevoegenCategorie(selectedCategorie);

            if (!ValidatieCategorie()) return;

            if (result)
            {
                RefreshCategorie();
                SelectedCategorie = new Categorie();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het toevoegen van de categorie", "OK");
            }
        }


        [RelayCommand]
        public void Wijzigen()
        {
            var result = _categorieRepository.WijzigenCategorie(selectedCategorie);

            if (!ValidatieCategorie()) return;

            if (result)
            {
                RefreshCategorie();
                SelectedCategorie = new Categorie();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het wijzigen van de categorie", "OK");
            }
        }


        [RelayCommand]
        public void Verwijderen()
        {
            var result = _categorieRepository.VerwijderenCategorie(SelectedCategorie.Id);

            if (result)
            {
                RefreshCategorie();
                SelectedCategorie = new Categorie();
            }
            else
            {
                Shell.Current.DisplayAlert("Fout", "Er is een fout opgetreden bij het verwijderen van de categorie", "OK");
            }
        }

        [RelayCommand]
        public void Deselecteren()
        {
            SelectedCategorie = new Categorie();
        }
    }
}