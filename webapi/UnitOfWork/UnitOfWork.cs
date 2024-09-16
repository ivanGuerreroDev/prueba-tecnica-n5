using Microsoft.EntityFrameworkCore;
using webapi.Models;
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly IRepository<Permission> _permissionsRepository;

    public UnitOfWork(DbContext context)
    {
        _context = context;
        _permissionsRepository = new Repository<Permission>(_context);
    }

    public IRepository<Permission> Permissions => _permissionsRepository;

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}