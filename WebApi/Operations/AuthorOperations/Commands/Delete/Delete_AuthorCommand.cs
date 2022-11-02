using WebApi.DBOperations;

namespace WebApi.Operations.AuthorOperations.Delete.Commands
{
    public class DeleteAuthorCommand
    {
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;

        public DeleteAuthorCommand(IBookStoreDbContext dbContext, int itemId)
        {
            _dbContext = dbContext;
            ID = itemId;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(s => s.Id == ID);
            if (author is null)
                throw new AppException("Author not found");

            // TODO: Yazarın kitap kontrolü yapılacak.
            // Kitabı olan bir yazar silinmemeli (yayında olsun ya da olmasın).
            var authorBooksCheck = (
                from ab in _dbContext.BookAuthors.Where(w => w.AuthorId == author.Id)
                from b in _dbContext.Books.Where(w => w.Id == ab.BookId)
                select b
            ).ToList();
            if (authorBooksCheck.Count() > 0)
            {
                throw new AppException(
                    $"The author you are trying to delete has {authorBooksCheck.Count()} book(s) in publication. Please delete the books first."
                );
            }

            _dbContext.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
