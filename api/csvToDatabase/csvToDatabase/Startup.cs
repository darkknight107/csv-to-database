using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using business.Commands;
using business.Entity;
using business.Queries;
using business.Services;
using core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace csvToDatabase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<ICqrsDispatcher, CqrsDispatcher>();
            services.AddScoped<IQueryHandler<GetTablesQuery, List<string>>, GetTablesQueryHandler>();
            services.AddScoped<IQueryHandler<GetColumnsQuery, List<string>>, GetColumnsQueryHandler>();
            services.AddScoped<IQueryHandler<ExtractCsvHeadersQuery, List<string>>, ExtractCsvHeadersQueryHandler>();
            services.AddScoped<ICsvParser<List<string>>, CsvFileParser>();
            services.AddScoped<ICommandHandler<PostCsvToTableCommand>, PostCsvToTableCommandHandler>();
            AddCommandQueryHandlers(services, typeof(IQueryHandler<,>));
            AddCommandQueryHandlers(services, typeof(ICommandHandler<>));
            services.AddScoped(typeof(IDbConnector<>), typeof(DapperConnector<>));
        }

        private static void AddCommandQueryHandlers(IServiceCollection services, Type handlerInterface)
        {
            var handlers = typeof(Startup)
                .Assembly
                .GetTypes()  
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)  
                );   
            
            foreach (var handler in handlers)
            {
                services.AddScoped(
                    handler.GetInterfaces()
                        .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}