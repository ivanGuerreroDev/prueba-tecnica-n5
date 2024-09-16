using System;
public interface IUnitOfWork : IDisposable
{
    IRepository<Permission> Permissions { get; }
    void Save();
}