using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.MappingProfiles;

namespace WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDb")
                .Options;
            Context.Database.EnsureCreated();

            Context.AddMockGenres();
            Context.AddMockBooks();
            Context.AddMockAuthors();
            Context.AddMockBookAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            }).CreateMapper();
        }
    }
}
