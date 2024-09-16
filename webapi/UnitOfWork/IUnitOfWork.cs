using System;
using webapi.Models;
public interface IUnitOfWork : IDisposable
{
    IRepository<Permission> Permissions { get; }
    void Save();
}