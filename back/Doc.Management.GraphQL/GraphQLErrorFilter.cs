using HotChocolate;
using Microsoft.Extensions.Logging;

namespace Doc.Management.GraphQL;

public class GraphQLErrorFilter : IErrorFilter
{
    private readonly ILogger<GraphQLErrorFilter> _logger;

    public GraphQLErrorFilter(ILogger<GraphQLErrorFilter> logger)
    {
        _logger = logger;
    }

    public IError OnError(IError error)
    {
        _logger.LogError(error.Exception, error.Exception?.Message ?? error.Message);

        return error;
    }
}
