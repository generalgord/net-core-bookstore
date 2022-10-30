using AutoMapper;
using WebApi.Entities;
using WebApi.Operations.BookOperations.Commands;

namespace WebApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
        }
    }
}
