using System;
using Journalist.Crm.Marten.Clients;
using Marten;
using Marten.Events;
using Marten.Events.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using Doc.Management.Marten.Documents;
using Doc.Management.Documents.DataModels;
using Doc.Management.CQRS;
using Doc.Management.Documents;

namespace Doc.Management.Marten;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocManagementMarten(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("Marten") ?? throw new ArgumentException("missing connection string"));

            // Events
            options.Events.StreamIdentity = StreamIdentity.AsString;

            // Projections
            options.Projections.Add(new DocumentProjection(), ProjectionLifecycle.Inline);

            // Indexes
            options.Schema.For<DocumentDocument>().UniqueIndex(c => c.Id);
            options.Schema.For<DocumentDocument>().UniqueIndex(c => c.Key);
            options.Schema.For<DocumentDocument>().FullTextIndex(c => c.FileNameWithoutExtension);
        });

        var querySessionDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IQuerySession));

        if (querySessionDescriptor != null)
        {
            services.Remove(querySessionDescriptor);
        }
        services.AddTransient(s => s.GetRequiredService<ISessionFactory>().QuerySession());

        var documentSessionDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IDocumentSession));

        if (documentSessionDescriptor != null)
        {
            services.Remove(documentSessionDescriptor);
        }
        services.AddTransient(s => s.GetRequiredService<ISessionFactory>().OpenSession());

        services.AddTransient<AggregateRepository>();
        services.AddTransient<IWriteEvents>(sp => sp.GetRequiredService<AggregateRepository>());
        services.AddTransient<IReadAggregates>(sp => sp.GetRequiredService<AggregateRepository>());
        services.AddTransient<IReadDocuments, DocumentRepository>();

        return services;
    }
}
