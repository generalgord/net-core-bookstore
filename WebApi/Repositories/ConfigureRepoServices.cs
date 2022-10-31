using WebApi.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureRepoServices
{
    public static void AddRepoServices(this IServiceCollection services)
    {
        GenreRepository.Initialize();
        BookRepository.Initialize();
    }
}
