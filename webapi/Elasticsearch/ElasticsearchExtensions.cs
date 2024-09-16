using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

public static class ElasticsearchExtensions
{
    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("ElasticsSearchSettings");
        var url = settings["Url"];
        var defaultIndex = settings["DefaultIndex"];

        var connectionSettings = new ConnectionSettings(new Uri(url))
            .DefaultIndex(defaultIndex);

        var elasticClient = new ElasticClient(connectionSettings);

        services.AddSingleton<IElasticClient>(elasticClient);

        return services;
    }
}