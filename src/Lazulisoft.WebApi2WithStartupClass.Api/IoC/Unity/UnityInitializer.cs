using Lazulisoft.WebApi2WithStartupClass.Api.Data;
using Lazulisoft.WebApi2WithStartupClass.Api.Repositories;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace Lazulisoft.WebApi2WithStartupClass.Api.IoC.Unity
{
    public static class UnityInitializer
    {
        public static void Initialize(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // Registers          
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}