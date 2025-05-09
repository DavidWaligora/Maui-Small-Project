using ITC2Wedstrijd.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC2Wedstrijd.ViewModels
{
    public partial class TeamsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public List<Team> teams;

        private APIControllerTeams _apiController;
        public TeamsViewModel(APIControllerTeams apiController) {
            Title = "Teams API";
            _apiController = apiController;
        }
        [RelayCommand]
        private async void TeamsOphalen()
        {
            Teams = await APIControllerTeams.GetTeams();
        }
    }
}
