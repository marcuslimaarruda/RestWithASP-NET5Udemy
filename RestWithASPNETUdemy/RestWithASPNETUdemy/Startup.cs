using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Business.Implementations;
using Serilog;
using System;
using System.Collections.Generic;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Enviroment { get;  }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Enviroment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
           
        }

    
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
  
            services.AddControllers();

            // Conexão com MySQL
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            // Serviço para criar o database se ele não existir.
            if (Enviroment.IsDevelopment())
            {
               // MigrateDatabase(connection);
            }

            // Serviço de verssionamento
            services.AddApiVersioning();

            //Injeção de dependencia para o repository generico
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            //Injeção de dependencia para person
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            //services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

            ////Injeção de dependencia para book
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            //services.AddScoped<IBookRepository, BookRepositoryImplementation>();

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
            });
        }

        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Erro criando banco inicial em desenvolvimento", ex);
                throw;
            }
        }
    }
}
