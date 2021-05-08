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
using Microsoft.Net.Http.Headers;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        private RewriteOptions option; // Criada para uso na carga do swagger

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

            // Conex�o com MySQL
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            // Servi�o para criar o database se ele n�o existir.
            if (Enviroment.IsDevelopment())
            {
               // MigrateDatabase(connection);
            }

            // Adcionando novo servi�o para convers�o para XML.
            services.AddMvc(options =>
           {
               options.RespectBrowserAcceptHeader = true;

               options.FormatterMappings.SetMediaTypeMappingForFormat("xnl", MediaTypeHeaderValue.Parse("aplication/xml"));
               options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("aplication/json"));

           })
            .AddXmlSerializerFormatters();

            // Inje��o do servi�o HATEOAS Hypermedia.
            var filterOption = new HypeMediaFiltersOptions();
            filterOption.ContentResponseEnricherList.Add(new PersonEnricher());

            services.AddSingleton(filterOption);

            // Servi�o de verssionamento
            services.AddApiVersioning();

            // Adcionado o Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API do zero com DotNet Core 5 e Docker",
                        Version = "v1",
                        Description = "Curso para constru��o de 'REST API do zero com DotNet Core 5 e Docker'",
                        Contact = new OpenApiContact
                        {
                            Name = "Marcus Lima Arruda",
                            Url = new Uri("https://github.com/marcuslimaarruda")
                        }
                    });
            });

            //Inje��o de dependencia para o repository generico
                services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            //Inje��o de dependencia para person
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            //services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

            ////Inje��o de dependencia para book
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
            // Adcionado o Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Rest APIs From 0 to Azure with ASP.NET and Docker");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$","swagger");

            app.UseRewriter(option);

            //------------
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}"); // HATEOAS
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
