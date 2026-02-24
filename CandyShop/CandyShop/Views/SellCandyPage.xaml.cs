namespace CandyShop.Views;

public partial class SellCandyPage : ContentPage
{
	public SellCandyViewModel sellCandyViewModel => BindingContext as SellCandyViewModel;
	public SellCandyPage(SellCandyViewModel sellCandyViewModel)
	{
		BindingContext = sellCandyViewModel;
		InitializeComponent();
	}
}