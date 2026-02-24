namespace CandyShop.ViewModels;

public partial class SellCandyViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> products;

    [ObservableProperty]
    private ObservableCollection<Order> orders;

    [ObservableProperty]
    private ObservableCollection<OrderItem> orderItems;

    [ObservableProperty]
    private Product? selectedCandyForSell = new();

    [ObservableProperty]
    private int? candyQuantity;

    private readonly FileService fileService;
    public SellCandyViewModel(FileService fileService)
	{
        this.fileService = fileService;
        Products = [];
        Orders = [];
        OrderItems = [];
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
    private void AddItemToOrder()
    {
        if (SelectedCandyForSell == null || CandyQuantity == null || CandyQuantity <= 0)
            return;

        var product = Products.FirstOrDefault(p => p.Id == SelectedCandyForSell.Id);
        if (product == null)
            return;

        if (CandyQuantity > product.Stock)
            return;

        OrderItems.Add(new OrderItem
        {
            ProductId = product.Id,
            Name = product.Name,
            Quantity = (int)CandyQuantity,
            UnitPrice = product.Price
        });

        product.Stock -= (int)CandyQuantity;
        SelectedCandyForSell = null;
        CandyQuantity = null;
    }

    [RelayCommand]
    private void SellCandy()
    {
        if (OrderItems.Count == 0)
            return;

        Orders.Add(new Order
        {
            Id = Orders.Count > 0 ? Orders.Max(o => o.Id) + 1 : 1,
            Date =  DateTime.Now,
            Items = OrderItems.ToList(),
            TotalAmount = OrderItems.Sum(o => o.Quantity * o.UnitPrice)
        });

        OrderItems = [];
        fileService.SaveData(Products.ToList(), Orders.ToList());
    }
}