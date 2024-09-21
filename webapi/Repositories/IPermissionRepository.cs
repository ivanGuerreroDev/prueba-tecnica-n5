using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IPermissionRepository
    {
        Task<Permission> GetPermissionByIdAsync(int id);
        Task AddPermissionAsync(Permission permission);
        Task UpdatePermissionAsync(Permission permission);
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task GetByIdAsync(object permissionId);
        void Update(Permission permission);
    }
}
