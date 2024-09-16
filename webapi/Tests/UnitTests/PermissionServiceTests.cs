using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

public class PermissionServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<PermissionElasticSearchService> _elasticSearchServiceMock;
    private readonly PermissionService _permissionService;

    public PermissionServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _elasticSearchServiceMock = new Mock<PermissionElasticSearchService>();
        _permissionService = new PermissionService(_unitOfWorkMock.Object, _elasticSearchServiceMock.Object);
    }

    [Fact]
    public async Task RequestPermissionAsync_Should_Add_Permission_And_Index_ElasticSearch()
    {
        var permission = new Permission
        {
            Id = 1,
            EmployeeName = "John",
            EmployeeLastName = "Doe",
            PermissionTypeId = 1,
            PermissionDate = System.DateTime.UtcNow
        };

        _unitOfWorkMock.Setup(u => u.Permissions.AddPermission(permission)).Verifiable();
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask).Verifiable();
        _elasticSearchServiceMock.Setup(e => e.IndexPermissionAsync(It.IsAny<PermissionDocument>())).Returns(Task.CompletedTask).Verifiable();

        await _permissionService.RequestPermissionAsync(permission);

        _unitOfWorkMock.Verify(u => u.Permissions.AddPermission(permission), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        _elasticSearchServiceMock.Verify(e => e.IndexPermissionAsync(It.IsAny<PermissionDocument>()), Times.Once);
    }

    [Fact]
    public async Task ModifyPermissionAsync_Should_Update_Permission_And_Index_ElasticSearch()
    {
        var permission = new Permission
        {
            Id = 1,
            EmployeeName = "John",
            EmployeeLastName = "Doe",
            PermissionTypeId = 2,
            PermissionDate = System.DateTime.UtcNow
        };

        _unitOfWorkMock.Setup(u => u.Permissions.UpdatePermission(permission)).Verifiable();
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask).Verifiable();
        _elasticSearchServiceMock.Setup(e => e.IndexPermissionAsync(It.IsAny<PermissionDocument>())).Returns(Task.CompletedTask).Verifiable();

        await _permissionService.ModifyPermissionAsync(permission);

        _unitOfWorkMock.Verify(u => u.Permissions.UpdatePermission(permission), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        _elasticSearchServiceMock.Verify(e => e.IndexPermissionAsync(It.IsAny<PermissionDocument>()), Times.Once);
    }

    [Fact]
    public async Task GetPermissionsAsync_Should_Return_Permissions()
    {
        var permissions = new List<Permission>
        {
            new Permission { Id = 1, EmployeeName = "John", EmployeeLastName = "Doe" },
            new Permission { Id = 2, EmployeeName = "Jane", EmployeeLastName = "Smith" }
        };

        _unitOfWorkMock.Setup(u => u.Permissions.GetAllPermissionsAsync()).ReturnsAsync(permissions);

        var result = await _permissionService.GetPermissionsAsync();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.EmployeeName == "John");
        Assert.Contains(result, p => p.EmployeeName == "Jane");
    }
}
