namespace CandyShop.ViewModels;

public partial class ReportViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> products;

    [ObservableProperty]
    private ObservableCollection<Order> orders;

    [ObservableProperty]
    private decimal totalRevenueV;

    [ObservableProperty]
    private string? mSoldProduct;

    [ObservableProperty]
    private List<Product>? nSoldProduct;

    private readonly FileService fileService;
    public ReportViewModel(FileService fileService)
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

            TotalRevenueV = TotalRevenue(Orders.ToList());
            MSoldProduct = MostSoldProduct(Orders.ToList());
            NSoldProduct = NotSoldProducts(Orders.ToList(), Products.ToList());
        }
    }

    public decimal TotalRevenue(List<Order> orders)
    {
        var totalRevenue = orders.Sum(o => o.TotalAmount);
        return totalRevenue;
    }

    public string MostSoldProduct(List<Order> orders)
    {
        var productSales = new Dictionary<string, int>();
        foreach (var order in orders)
        {
            foreach (var item in order.Items)
            {
                if (productSales.ContainsKey(item.Name))
                {
                    productSales[item.Name] += item.Quantity;
                }
                else
                {
                    productSales[item.Name] = item.Quantity;
                }
            }
        }
        if (productSales.Count == 0)
        {
            return null;
        }
        var mostSoldProduct = productSales.MaxBy(p => p.Value);
        var toString = $"{mostSoldProduct.Key} - {mostSoldProduct.Value} db";
        return toString;
    }

    public List<Product>? NotSoldProducts(List<Order> orders, List<Product> products)
    {
        var soldProductIds = new List<int>();
        orders.ForEach(order =>
        {
            order.Items.ForEach(item =>
            {
                if (!soldProductIds.Contains(item.ProductId))
                {
                    soldProductIds.Add(item.ProductId);
                }
            });
        });
        var notSoldProducts = products.Where(p => !soldProductIds.Contains(p.Id)).ToList();
        if (notSoldProducts.Count == 0)
        {
            return null;
        }
        return notSoldProducts;
    }
}