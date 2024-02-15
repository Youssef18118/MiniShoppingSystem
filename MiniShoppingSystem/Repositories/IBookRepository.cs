namespace MiniShoppingSystem.Repositories
{
    public interface IBookRepository
    {
        public void AddBook(Book book);
        public void EditBook(Book book);
        public void RemoveBook(Book book);
    }
}
