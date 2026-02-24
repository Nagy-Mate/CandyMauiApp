namespace CandyShop.Services;

public class FileService
{
    private readonly string filePath = "data.json";

    public Data LoadData()
    {
        var jsonData = File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<Data>(jsonData);
        return data;
    }

    public void SaveData(List<Product> products, List<Order> orders)
    {
        var data = new Data
        {
            Products = products,
            Orders = orders
        };
        var jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, jsonData);
    }
}
