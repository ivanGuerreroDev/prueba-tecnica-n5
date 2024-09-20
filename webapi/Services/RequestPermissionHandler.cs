using WebApi.Repositories;
using WebApi.Models;
using WebApi.ElasticSearch;
using WebApi.Kafka;
using WebApi.Services;

namespace WebApi.Services
{
    public class RequestPermissionHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IKafkaProducer _kafkaProducer;

        public RequestPermissionHandler(IUnitOfWork unitOfWork, IElasticSearchService elasticSearchService, IKafkaProducer kafkaProducer)
        {
            _unitOfWork = unitOfWork;
            _elasticSearchService = elasticSearchService;
            _kafkaProducer = kafkaProducer;
        }

        public async Task Handle(RequestPermissionCommand command)
        {
            var permission = new Permission
            {
                UserId = command.UserId,
                PermissionTypeId = command.PermissionTypeId,
                DateRequested = DateTime.UtcNow
            };

            await _unitOfWork.Permissions.AddPermissionAsync(permission);
            await _unitOfWork.CommitAsync();

            await _elasticSearchService.IndexPermissionAsync(permission);
            await _kafkaProducer.SendMessageAsync("request", permission.Id.ToString());
        }
    }
}
