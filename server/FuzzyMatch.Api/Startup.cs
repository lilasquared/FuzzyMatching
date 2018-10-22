using System;
using System.Linq;
using FuzzyMatch.Api.Abstracts;
using FuzzyMatch.Api.Conventions;
using FuzzyMatch.Api.Providers;
using FuzzyMatch.Core;
using MediatR;
using MediatR.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info {Title = "API", Version = "v1"});
                config.TagActionsBy(api =>
                {
                    api.TryGetMethodInfo(out var info);

                    return info.DeclaringType.GetGenericArguments()?[0]?.Name ?? info.DeclaringType.Name;
                });
            });

            var types = new Container(config =>
                {
                    config.Scan(scan =>
                    {
                        scan.AssembliesFromApplicationBaseDirectory();

                        scan.AddAllTypesOf<IControllable>();
                    });
                })
                .GetAllInstances<IControllable>()
                .Select(x => x.GetType())
                .ToList();

            var featureProvider = new DynamicControllerFeatureProvider(typeof(BaseController<>), types);

            services.AddMvc(o => o.Conventions.Add(new DynamicControllerRouteConvention()))
                    .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(featureProvider))
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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

                config.AddGenericHandlers(types, new []
                {
                    typeof(GetAllHandler<>),
                    typeof(GetOneHandler<>),
                    typeof(CreateHandler<>),
                    typeof(DeleteHandler<>)
                });

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
            app.UseMvc();
        }
    }
}
