using Elasticsearch.Net;
using Nest;
using WebApi.Models;

namespace WebApi.ElasticSearch
{
    public interface IElasticSearchService
    {
        Task IndexPermissionAsync(Permission permission);
    }

    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("permissions");
            _elasticClient = new ElasticClient(settings);
        }

        public async Task IndexPermissionAsync(Permission permission)
        {
            var response = await _elasticClient.IndexDocumentAsync(permission);
            if (!response.IsValid)
            {
                throw new Exception("Failed to index document in Elasticsearch");
            }
        }
    }
}
