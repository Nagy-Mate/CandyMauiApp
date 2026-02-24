namespace CandyShop.ViewModels;

public partial class EditPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> products;

    [ObservableProperty]
    private ObservableCollection<Order> orders;

    [ObservableProperty]
    private Product? selectedCandyForUpdate = new();

    [ObservableProperty]
    private string? newName;

    [ObservableProperty]
    private decimal? newPrice;

    [ObservableProperty]
    private int? newStock;

    private readonly FileService fileService;
    public EditPageViewModel(FileService fileService)
	{
        this.fileService = fileService;
        Products = [];
        Orders = [];
        LoadProducts();
    }
    private void LoadProducts()
    {
        var data = fileService.LoadData();

        if (data != null)
        {
            Products.Clear();
            Products = data.Products.ToObservableCollection();
            Orders.Clear();
            Orders = data.Orders.ToObservableCollection();
        }
    }

    [RelayCommand]
    private void UpdateCandy()
    {
        if (NewPrice == null && NewStock == null && NewName == null || SelectedCandyForUpdate == null)
            return;

        var product = Products.FirstOrDefault(p => p.Id == SelectedCandyForUpdate.Id);

        if (product == null)
            return;

        if(NewName != null && !string.IsNullOrWhiteSpace(NewName))
        {
            product.Name = NewName;
        }

        if(NewPrice != null && NewPrice >= 0)
        {
            product.Price = (decimal)NewPrice;
        }

        if (NewStock != null && NewStock >= 0)
        {
            product.Stock = (int)NewStock;
        }

        SelectedCandyForUpdate = null;
        NewName = null;
        NewPrice = null;
        NewStock = null;

        fileService.SaveData(Products.ToList(), Orders.ToList());

    }
}