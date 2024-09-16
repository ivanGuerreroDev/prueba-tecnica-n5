using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace webapi.Repositories
{

    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.Include(p => p.PermissionType).ToListAsync();
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions.Include(p => p.PermissionType).FirstOrDefaultAsync(p => p.Id == id);
        }

        public void AddPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
        }

        public void UpdatePermission(Permission permission)
        {
            _context.Permissions.Update(permission);
        }
    }
}