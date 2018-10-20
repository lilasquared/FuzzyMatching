using System;
using FuzzyMatch.Api.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;

namespace FuzzyMatch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });

            var container = new Container();
            container.Configure(config =>
            {
                config.Scan(scan =>
                {
                    scan.AssembliesFromApplicationBaseDirectory();
                    scan.WithDefaultConventions();

                    scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                });

                config.For<IMediator>().Use<Mediator>();
                config.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
                config.For(typeof(IPipelineBehavior<,>)).Add(typeof(ExceptionHandlerBehavior<,>));

                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            app.MapWhen(
                context => context.Request.Path.Value.StartsWith("/api", StringComparison.InvariantCultureIgnoreCase),
                branch => branch.UseMvc());

            app.MapWhen(
                context => !context.Request.Path.Value.Contains("."),
                branch =>
                {
                    branch.Use((context, next) =>
                    {
                        context.Request.Path = new Microsoft.AspNetCore.Http.PathString("/index.html");
                        return next();
                    });
                    branch.UseStaticFiles();
                }
            );

            app.UseStaticFiles();
        }
    }
}
