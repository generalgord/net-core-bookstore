using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPublished { get; set; } = true;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public IList<BookAuthor> BookAuthors { get; set; } = null!;
    }
}
