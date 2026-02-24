namespace CandyShop.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<Product> products;

	private readonly FileService fileService;
	public MainPageViewModel(FileService fileService)
	{
		this.fileService = fileService;
		Products = [];
		LoadProducts();
	}

	private void LoadProducts()
	{
		var data = fileService.LoadData();

		if(data.Products != null)
		{
			Products.Clear();
            Products = data.Products.ToObservableCollection();
        }
	}
}