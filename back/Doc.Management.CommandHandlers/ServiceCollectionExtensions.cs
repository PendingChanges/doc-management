using Doc.Management.CommandHandlers.Documents;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Management.CommandHandlers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(CreateDocumentHandler).Assembly);
        });

        return services;
    }
}
