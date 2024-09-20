using WebApi.Data;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions.Include(p => p.PermissionType).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPermissionAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public async Task UpdatePermissionAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.Include(p => p.PermissionType).ToListAsync();
        }
    }
}
