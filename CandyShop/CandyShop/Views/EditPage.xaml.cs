namespace CandyShop.Views;

public partial class EditPage : ContentPage
{
	public EditPageViewModel editPageViewModel => BindingContext as EditPageViewModel;
	public EditPage(EditPageViewModel editPageViewModel)
	{
		BindingContext = editPageViewModel;
		InitializeComponent();
	}
}