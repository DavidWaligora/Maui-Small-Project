namespace ITC2Wedstrijd.Views;

public partial class TeamsPage : ContentPage
{
	public TeamsPage(TeamsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}