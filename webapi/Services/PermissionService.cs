public class PermissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PermissionElasticSearchService _elasticSearchService;

    public PermissionService(IUnitOfWork unitOfWork, PermissionElasticSearchService elasticSearchService)
    {
        _unitOfWork = unitOfWork;
        _elasticSearchService = elasticSearchService;
    }

    public async Task RequestPermissionAsync(Permission permission)
    {
        _unitOfWork.Permissions.AddPermission(permission);
        await _unitOfWork.CompleteAsync();

        var permissionDocument = new PermissionDocument
        {
            Id = permission.Id,
            EmployeeName = permission.EmployeeName,
            EmployeeLastName = permission.EmployeeLastName,
            PermissionTypeId = permission.PermissionTypeId,
            PermissionDate = permission.PermissionDate,
            PermissionTypeDescription = (await _unitOfWork.Permissions.GetPermissionByIdAsync(permission.Id)).PermissionType.Description
        };

        await _elasticSearchService.IndexPermissionAsync(permissionDocument);
    }

    public async Task ModifyPermissionAsync(Permission permission)
    {
        _unitOfWork.Permissions.UpdatePermission(permission);
        await _unitOfWork.CompleteAsync();

        var permissionDocument = new PermissionDocument
        {
            Id = permission.Id,
            EmployeeName = permission.EmployeeName,
            EmployeeLastName = permission.EmployeeLastName,
            PermissionTypeId = permission.PermissionTypeId,
            PermissionDate = permission.PermissionDate,
            PermissionTypeDescription = (await _unitOfWork.Permissions.GetPermissionByIdAsync(permission.Id)).PermissionType.Description
        };

        await _elasticSearchService.IndexPermissionAsync(permissionDocument);
    }

    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        return await _unitOfWork.Permissions.GetAllPermissionsAsync();
    }
}
