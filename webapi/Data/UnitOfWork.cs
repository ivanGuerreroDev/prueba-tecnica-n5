using System.Threading.Tasks;
using webapi.Data;
namespace webapi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IPermissionRepository _permissionRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementación de la propiedad PermissionRepository
        public IPermissionRepository PermissionRepository
        {
            get
            {
                return _permissionRepository ??= new PermissionRepository(_context);
            }
        }

        // Implementación del método CompleteAsync
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Implementación de Dispose
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
