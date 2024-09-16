using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;

public class PermissionsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;

    public PermissionsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task RequestPermission_Should_Return_Ok()
    {
        var newPermission = new
        {
            EmployeeName = "John",
            EmployeeLastName = "Doe",
            PermissionTypeId = 1,
            PermissionDate = System.DateTime.UtcNow
        };

        var content = new StringContent(JsonConvert.SerializeObject(newPermission), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/permissions/request", content);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ModifyPermission_Should_Return_Ok()
    {
        var modifiedPermission = new
        {
            Id = 1,
            EmployeeName = "John",
            EmployeeLastName = "Doe",
            PermissionTypeId = 2,
            PermissionDate = System.DateTime.UtcNow
        };

        var content = new StringContent(JsonConvert.SerializeObject(modifiedPermission), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("/api/permissions/modify", content);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPermissions_Should_Return_Permission_List()
    {
        var response = await _client.GetAsync("/api/permissions");

        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var permissions = JsonConvert.DeserializeObject<List<Permission>>(responseContent);

        Assert.NotNull(permissions);
        Assert.True(permissions.Count > 0);
    }
}
