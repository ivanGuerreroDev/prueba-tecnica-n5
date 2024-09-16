
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using webapi.Models;
using webapi.Repositories;
using webapi.Services;
using webapi.UnitOfWork;

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    var builder = WebApplication.CreateBuilder(services);

    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
    builder.RegisterType<Repository<Permission>>().As<IRepository<Permission>>();
    builder.RegisterType<RequestPermissionCommand>().As<ICommand>();
    builder.RegisterType<GetPermissionsQuery>().As<IQuery<IEnumerable<Permission>>>();
    builder.RegisterType<RequestPermissionService>().As<IRequestPermissionService>();

    services.AddAutofac();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}