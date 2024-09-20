using WebApi.Controllers;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using WebApi.Models;

namespace WebApi.Tests.IntegrationTests
{
    public class PermissionsControllerTests
    {
        private readonly PermissionsController _controller;
        private readonly Mock<RequestPermissionHandler> _requestPermissionHandlerMock;
        private readonly Mock<GetPermissionsHandler> _getPermissionsHandlerMock;

        public PermissionsControllerTests()
        {
            _requestPermissionHandlerMock = new Mock<RequestPermissionHandler>();
            _getPermissionsHandlerMock = new Mock<GetPermissionsHandler>();
            _controller = new PermissionsController(_requestPermissionHandlerMock.Object, _getPermissionsHandlerMock.Object);
        }

        [Fact]
        public async Task ShouldReturnOkWhenPermissionRequested()
        {
            var command = new RequestPermissionCommand { UserId = 1, PermissionTypeId = 2 };

            var result = await _controller.RequestPermission(command);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        // Este test requiere que el método GetPermissions esté definido en PermissionsController
        [Fact]
        public async Task ShouldReturnPermissionsWhenQueried()
        {
            var query = new GetPermissionsQuery();
            _getPermissionsHandlerMock.Setup(gph => gph.Handle(query)).ReturnsAsync(new List<Permission> { new Permission() });

            var result = await _controller.GetPermissions(query);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}