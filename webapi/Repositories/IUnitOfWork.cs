namespace WebApi.Repositories
{
    public interface IUnitOfWork
    {
        IPermissionRepository Permissions { get; }
        Task<int> CommitAsync();
    }
}
