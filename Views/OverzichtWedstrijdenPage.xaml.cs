namespace ITC2Wedstrijd.Views;

public partial class OverzichtWedstrijdenPage : ContentPage
{
	public OverzichtWedstrijdenPage(OverzichtWedstrijdenViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}