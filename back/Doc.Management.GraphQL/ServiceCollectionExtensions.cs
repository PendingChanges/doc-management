using Doc.Management.GraphQL.Documents;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Management.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocManagementGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType(q => q.Name("Query"))
            .AddType<DocumentsQueries>();

        services.AddErrorFilter<GraphQLErrorFilter>();

        return services;
    }
}
