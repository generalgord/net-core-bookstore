using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.BookOperations.Update.Commands
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly IMapper _mapper;
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;

        public UpdateBookCommand(
            IBookStoreDbContext dbContext,
            IMapper mapper,
            int itemId,
            UpdateBookModel model
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            ID = itemId;
            Model = model;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Id == ID);
            if (book is null)
                throw new AppException("Book not found");

            if (_dbContext.Books.Any(a => a.Title.ToLower() == Model.Title.ToLower() && a.Id != ID))
                throw new AppException(
                    "Same name of Book is available. Please try another book title."
                );

            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;

            _dbContext.Books.Update(book);

            // Kitap başarıyla güncellenebiliyorsa yazarları da ayrı olarak kaydedelim.
            if (Model.Authors?.Count() > 0 && Model.Authors != null)
            {
                var authorsToBeAdded = new List<Author>();
                foreach (var item in Model.Authors)
                {
                    try
                    {
                        var authorCheck = _dbContext.Authors.FirstOrDefault(
                            s =>
                                (
                                    s.FirstName.ToLower() == item.FirstName.ToLower()
                                    && s.LastName.ToLower() == item.LastName.ToLower()
                                )
                                || s.Id == item.Id
                        );
                        if (authorCheck != null)
                        {
                            // yazar mevcutsa kitaba ekleyelim
                            // herhangi bir güncelleme yapmıyoruz
                            // güncelleme yapılacaksa yazar güncelleme kullanılabilir
                            authorsToBeAdded.Add(authorCheck);
                        }
                        else
                        {
                            // kitap mevcut değilse yeni yazar oluşturalım ve kitaba ekleyelim
                            authorCheck = _mapper.Map<Author>(item);
                            authorsToBeAdded.Add(authorCheck);
                        }
                    }
                    catch (System.Exception)
                    {
                        throw new AppException(
                            "The author you are trying to add to a book does not exist. Please add the author first."
                        );
                    }
                }
                foreach (var author in authorsToBeAdded)
                {
                    _dbContext.BookAuthors.Add(new BookAuthor() { Book = book, Author = author });
                }
            }

            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while updating the book.");
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public List<AddAuthorToBookModel>? Authors { get; set; }
    }

    public class AddAuthorToBookModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
    }
}
