using AutoMapper;
using WebApi.Common;
using WebApi.Entities;
using WebApi.Operations.BookOperations.Commands;
using WebApi.Operations.BookOperations.Queries;

namespace WebApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModel>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())
                )
                .ForMember(
                    dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy"))
                );
        }
    }
}
