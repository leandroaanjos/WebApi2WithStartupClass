using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Lazulisoft.WebApi2WithStartupClass.Api.Data;
using Lazulisoft.WebApi2WithStartupClass.Api.IoC.Unity;
using Lazulisoft.WebApi2WithStartupClass.Api.Models;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Lazulisoft.WebApi2WithStartupClass.Api.Startup))]

namespace Lazulisoft.WebApi2WithStartupClass.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //  Enable attribute based routing
            config.MapHttpAttributeRoutes();

            // Default route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigureWebApi(config);

            // Package: Unity.Container
            // Unity DI
            UnityInitializer.Initialize(config);

            app.UseWebApi(config);

            // Create the database for test (REMOVE when publish to PROD)
            InitializeDatabaseAsync(config).GetAwaiter().GetResult();
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // JSON serialize settings
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private async Task InitializeDatabaseAsync(HttpConfiguration config)
        {
            var dbContext = config.DependencyResolver.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext; // new ApplicationDbContext();
            dbContext.Database.CreateIfNotExists();

            if (dbContext.Database.Exists() && !dbContext.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "XBox One", Price = 299.00M, Category = ProductCategory.VideoGames },
                    new Product { Name = "PlayStation 4", Price = 320.00M, Category = ProductCategory.VideoGames },
                    new Product { Name = "Switch", Price = 249.00M, Category = ProductCategory.VideoGames },
                    new Product { Name = "3DS", Price = 199.00M, Category = ProductCategory.VideoGames },
                    new Product { Name = "PS Vita", Price = 189.00M, Category = ProductCategory.VideoGames },
                };

                products.ForEach(x => dbContext.Products.Add(x));
                await dbContext.SaveChangesAsync();

            }
        }
    }
}