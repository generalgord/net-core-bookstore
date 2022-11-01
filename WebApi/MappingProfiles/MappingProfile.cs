using AutoMapper;
using WebApi.Entities;
using WebApi.Operations.AuthorOperations.Commands;
using WebApi.Operations.AuthorOperations.Queries;
using WebApi.Operations.BookOperations.Commands;
using WebApi.Operations.BookOperations.Queries;
using WebApi.Operations.GenreOperations.Commands;
using WebApi.Operations.GenreOperations.Queries;

namespace WebApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book Mapping
            CreateMap<CreateBookModel, Book>();
            CreateMap<UpdateBookModel, Book>();
            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(
                    dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy"))
                );
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(
                    dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy"))
                );

            // Genre mapping
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<UpdateGenreModel, Genre>();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            // Author mapping
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>()
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.Date.ToString("dd/MM/yyyy"))
                );
            CreateMap<Author, AuthorDetailViewModel>()
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.Date.ToString("dd/MM/yyyy"))
                );

            // Add book to author mapping
            CreateMap<AddBookToAuthorModel, Book>();
            // Add author to book mapping
            CreateMap<AddAuthorToBookModel, Author>();
        }
    }
}
