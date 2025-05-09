namespace ITC2Wedstrijd.Views;

public partial class OverzichtPloegenPage : ContentPage
{
	public OverzichtPloegenPage(OverzichtPloegenViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}