using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
using Moq;
using Xunit;

namespace WebApi.Tests.UnitTests
{
    public class GetPermissionsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetPermissionsHandler _handler;

        public GetPermissionsHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetPermissionsHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ShouldReturnAllPermissionsWhenNoUserIdProvided()
        {
            var query = new GetPermissionsQuery();
            var permissions = new List<Permission> { new Permission(), new Permission() };

            _unitOfWorkMock.Setup(uow => uow.Permissions.GetAllPermissionsAsync()).ReturnsAsync(permissions);

            var result = await _handler.Handle(query);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ShouldReturnEmptyWhenNoPermissionsExist()
        {
            var query = new GetPermissionsQuery();
            var permissions = new List<Permission>();

            _unitOfWorkMock.Setup(uow => uow.Permissions.GetAllPermissionsAsync()).ReturnsAsync(permissions);

            var result = await _handler.Handle(query);

            Assert.Empty(result);
        }
    }
}
