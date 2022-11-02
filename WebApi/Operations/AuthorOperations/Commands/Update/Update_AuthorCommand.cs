using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.AuthorOperations.Update.Commands
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        private readonly IMapper _mapper;
        public int ID { get; set; }
        readonly IBookStoreDbContext _dbContext;

        public UpdateAuthorCommand(
            IBookStoreDbContext dbContext,
            IMapper mapper,
            int itemId,
            UpdateAuthorModel model
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            ID = itemId;
            Model = model;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(s => s.Id == ID);
            if (author is null)
                throw new AppException("Author not found");

            if (
                _dbContext.Authors.Any(
                    a =>
                        a.FirstName.ToLower() == Model.FirstName.ToLower()
                        && a.LastName.ToLower() == Model.LastName.ToLower()
                        && a.Id != ID
                )
            )
                throw new AppException(
                    "Same name of Author is available. Please try another author name."
                );

            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.DateOfBirth = Model.DateOfBirth;

            _dbContext.Authors.Update(author);

            // Yazar başarıyla güncellenebiliyorsa kitaplarını ayrı olarak kaydedelim.
            if (Model.Books?.Count() > 0 && Model.Books != null)
            {
                var booksToBeAdded = new List<Book>();
                foreach (var item in Model.Books)
                {
                    try
                    {
                        var bookCheck = _dbContext.Books.FirstOrDefault(
                            s => s.Title.ToLower() == item.Title.ToLower() || s.Id == item.Id
                        );
                        if (bookCheck != null)
                        {
                            // kitap mevcutsa yazara ekleyelim
                            // herhangi bir güncelleme yapmıyoruz
                            // güncelleme yapılacaksa kitap güncelleme kullanılabilir
                            booksToBeAdded.Add(bookCheck);
                        }
                        else
                        {
                            // kitap mevcut değilse yeni kitap oluşturalım ve yazara ekleyelim
                            bookCheck = _mapper.Map<Book>(item);
                            booksToBeAdded.Add(bookCheck);
                        }
                    }
                    catch (System.Exception)
                    {
                        throw new AppException(
                            "The book you are trying to add to a author does not exist. Please add the book first."
                        );
                    }
                }
                foreach (var book in booksToBeAdded)
                {
                    _dbContext.BookAuthors.Add(new BookAuthor() { Book = book, Author = author });
                }
            }
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while updating the author.");
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public List<AddBookToAuthorModel>? Books { get; set; }
    }

    public class AddBookToAuthorModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
