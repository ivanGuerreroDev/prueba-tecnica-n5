using System.Collections.Generic;
using webapi.Models;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Add(Permission permission);
}