using DataAccess.Models;

public interface IOrderService
{
    IEnumerable<Order> GetAllOrders();
    void UpdateOrderStatus(int orderId, string status);
    void PlaceOrder(Order order);
    Order GetOrderById(int orderId);
    IEnumerable<Order> GetOrdersByCustomerId(int customerId);
}

public interface IProductService
{
    IEnumerable<Paper> GetProducts(string search, string filter, string sort);
    void AddProduct(Paper paper);
    void DiscontinueProduct(int id);
    void RestockProduct(int id, int quantity);
    void AddCustomProperty(int paperId, string propertyName);
}