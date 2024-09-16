public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PermissionElasticSearchService _elasticSearchService;

    public RequestPermissionHandler(IUnitOfWork unitOfWork, PermissionElasticSearchService elasticSearchService)
    {
        _unitOfWork = unitOfWork;
        _elasticSearchService = elasticSearchService;
    }

    public async Task<Unit> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new Permission
        {
            EmployeeName = request.EmployeeName,
            EmployeeLastName = request.EmployeeLastName,
            PermissionTypeId = request.PermissionTypeId,
            PermissionDate = request.PermissionDate
        };

        _unitOfWork.Permissions.AddPermission(permission);
        await _unitOfWork.CompleteAsync();

        var document = new PermissionDocument
        {
            Id = permission.Id,
            EmployeeName = permission.EmployeeName,
            EmployeeLastName = permission.EmployeeLastName,
            PermissionTypeId = permission.PermissionTypeId,
            PermissionDate = permission.PermissionDate,
            PermissionTypeDescription = (await _unitOfWork.Permissions.GetPermissionByIdAsync(permission.Id)).PermissionType.Description
        };

        await _elasticSearchService.IndexPermissionAsync(document);
        return Unit.Value;
    }
}