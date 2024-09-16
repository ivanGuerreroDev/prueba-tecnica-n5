using Microsoft.EntityFrameworkCore;
using System;
using webapi.Models;

public class DbContext : IDisposable
{
    private readonly string _connectionString;

    public DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbContext()
    {
    }

    public DbSet<Permission> Permissions { get; set; }

    public void SaveChanges()
    {
        // Implement your database save logic here
    }

    public void Dispose()
    {
        // Implement your database dispose logic here
    }
}