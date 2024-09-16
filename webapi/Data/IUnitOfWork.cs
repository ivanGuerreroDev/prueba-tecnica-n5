using System.Threading.Tasks;
using System;

namespace webapi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository PermissionRepository { get; }
        Task<int> CompleteAsync();
    }
}

