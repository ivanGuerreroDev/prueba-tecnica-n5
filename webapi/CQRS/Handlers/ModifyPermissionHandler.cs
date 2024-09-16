public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PermissionElasticSearchService _elasticSearchService;

    public ModifyPermissionHandler(IUnitOfWork unitOfWork, PermissionElasticSearchService elasticSearchService)
    {
        _unitOfWork = unitOfWork;
        _elasticSearchService = elasticSearchService;
    }

    public async Task<Unit> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _unitOfWork.Permissions.GetPermissionByIdAsync(request.Id);

        if (permission == null)
        {
            throw new Exception("Permission not found");
        }

        permission.EmployeeName = request.EmployeeName;
        permission.EmployeeLastName = request.EmployeeLastName;
        permission.PermissionTypeId = request.PermissionTypeId;
        permission.PermissionDate = request.PermissionDate;

        _unitOfWork.Permissions.UpdatePermission(permission);
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