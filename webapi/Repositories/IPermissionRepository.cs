using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<Permission> GetPermissionByIdAsync(int id);
        void AddPermission(Permission permission);
        void UpdatePermission(Permission permission);
    }
}