using Nest;
using System;
using System.Threading.Tasks;
using webapi.Models;
public class PermissionElasticSearchService
{
    private readonly IElasticClient _elasticClient;

    public PermissionElasticSearchService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task IndexPermissionAsync(Permission document)
    {
        var response = await _elasticClient.IndexDocumentAsync(document);
        if (!response.IsValid)
        {
            throw new Exception("Error indexando en Elasticsearch");
        }
    }
}
