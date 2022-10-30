using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();
        public bool AddBook(Book item);
        public Book UpdateBook(int itemId, Book updatedItem);
        public bool RemoveBook(int itemId);
    }
}
