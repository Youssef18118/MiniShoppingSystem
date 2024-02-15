namespace MiniShoppingSystem.Repositories
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders();
        Task<IEnumerable<Order>> AllOrders();
    }
}
