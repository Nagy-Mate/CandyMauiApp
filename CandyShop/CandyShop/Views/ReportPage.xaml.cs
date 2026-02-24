namespace CandyShop.Views;

public partial class ReportPage : ContentPage
{
	public ReportViewModel reportViewModel => BindingContext as ReportViewModel;
	public ReportPage(ReportViewModel reportViewModel)
	{
		BindingContext = reportViewModel;
		InitializeComponent();
	}
}