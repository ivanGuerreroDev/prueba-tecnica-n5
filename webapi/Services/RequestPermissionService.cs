public class RequestPermissionService : IRequestPermissionService
{
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IUnitOfWork _unitOfWork;

    public RequestPermissionService(IKafkaProducer kafkaProducer, IUnitOfWork unitOfWork)
    {
        _kafkaProducer = kafkaProducer;
        _unitOfWork = unitOfWork;
    }

    public void RequestPermission(Permission permission)
    {
        _unitOfWork.Permissions.Add(permission);
        _unitOfWork.Save();

        var kafkaMessage = new { Id = Guid.NewGuid(), NameOperation = "request" };
        _kafkaProducer.SendMessage("permissions_topic", kafkaMessage);
    }
}