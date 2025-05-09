namespace ITC2Wedstrijd.Views;

public partial class WedstrijdenPage : ContentPage
{
	public WedstrijdenPage( WedstrijdenViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}