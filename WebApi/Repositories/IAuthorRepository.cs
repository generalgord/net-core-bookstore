using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface IAuthorRepository
    {
        public List<Author> GetAuthors();
        public bool AddAuthor(Author item);
        public Author UpdateAuthor(int itemId, Author updatedItem);
        public bool RemoveAuthor(int itemId);
    }
}
