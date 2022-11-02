using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Operations.BookOperations.Create.Commands
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(
            IBookStoreDbContext dbContext,
            IMapper mapper,
            CreateBookModel model
        )
        {
            _dbContext = dbContext;
            Model = model;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(s => s.Title == Model.Title);
            if (book is not null)
                throw new AppException("Book already added");

            book = _mapper.Map<Book>(Model);

            _dbContext.Books.Add(book);
            var isAdded = _dbContext.SaveChanges();
            if (isAdded <= 0)
                throw new AppException("An error occured while adding the book.");
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; } = "";
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
