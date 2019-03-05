using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Abstract;
using Sample.Infrastructure;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Sample.API
{
    public class Startup
    {
        Container SIContainer = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        const string CorsPolicyName = "AllowAllLocalhost";

        public void ConfigureServices(IServiceCollection services)
        {
            // We're setting up a CORS policy which allows XHR requests from any origin. I don't
            // know whether or not this CORS policy would be appropriate for production code, but
            // it works for our purposes. If I were writing production code, I would ask some more
            // knowledgeable coworkers if this CORS policy looks good.
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        CorsPolicyName,
                        builder =>
                        {
                            builder.AllowAnyOrigin();
                        });
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            IntegrateSimpleInjector(services);
        }

        void IntegrateSimpleInjector(IServiceCollection services)
        {
            // Boilerplate taken from
            // https://simpleinjector.readthedocs.io/en/latest/aspnetintegration.html

            SIContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(SIContainer));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(SIContainer));

            services.EnableSimpleInjectorCrossWiring(SIContainer);
            services.UseSimpleInjectorAspNetRequestScoping(SIContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(CorsPolicyName);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        void InitializeContainer(IApplicationBuilder app)
        {
            // Boilerplate taken from
            // https://simpleinjector.readthedocs.io/en/latest/aspnetintegration.html

            // Add application presentation components:
            SIContainer.RegisterMvcControllers(app);
            SIContainer.RegisterMvcViewComponents(app);

            // Add application services:
            SIContainer.Register<IGreetingService, GreetingService>(Lifestyle.Scoped);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            SIContainer.AutoCrossWireAspNetComponents(app);
        }
    }
}
