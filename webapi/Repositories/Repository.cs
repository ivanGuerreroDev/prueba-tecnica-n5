using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using webapi.Models;

public class Repository<T> : IRepository<T> where T : class
{

    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T Get(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Add(Permission permission)
    {
        throw new System.NotImplementedException();
    }
}