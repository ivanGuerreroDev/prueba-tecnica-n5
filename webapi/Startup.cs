using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapi.Models;

namespace prueba_tecnica_n5
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRepository<Permission>, Repository<Permission>>();
            services.AddTransient<IKafkaProducer, KafkaProducer>();
            services.AddTransient<IRequestPermissionService, RequestPermissionService>();
            services.AddElasticsearch(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var kafkaProducer = app.ApplicationServices.GetService<IKafkaProducer>();
            kafkaProducer.SendMessage("permissions_topic", "Hello, Kafka!");
        }
    }
}