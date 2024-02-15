namespace MiniShoppingSystem.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int bookId, int qty);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCarts> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCarts> GetCart(string userId);
        Task<bool> DoCheckout();
    }
}
