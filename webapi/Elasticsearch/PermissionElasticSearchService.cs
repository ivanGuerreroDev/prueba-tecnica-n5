using Nest;
using System;
using System.Threading.Tasks;
public class PermissionElasticSearchService
{
    private readonly IElasticClient _elasticClient;

    public PermissionElasticSearchService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task IndexPermissionAsync(PermissionDocument document)
    {
        var response = await _elasticClient.IndexDocumentAsync(document);
        if (!response.IsValid)
        {
            throw new Exception("Error indexando en Elasticsearch");
        }
    }
}
