using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.Middlerwares;
using Microsoft.Extensions.Logging;
using Domain.Repositories;
using Data.Repositories;
using Domain.Services;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options => {
                options.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });

            //Define conexão com a base dados
            services.AddEntityFrameworkNpgsql().AddDbContext<ObrasContext>((builder) => {
                builder.UseNpgsql(Configuration.GetConnectionString("ObrasBibliograficasConn"));
            }); 

            // Define repositórios
            services.AddScoped<INamesRepository, NamesRepository>();

            // Define serviços
            services.AddScoped<AuthorsService, AuthorsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors();
            app.UseGlobalExceptionHandler(loggerFactory);

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
