using System.Web.Http;
using Unity;

namespace Lazulisoft.WebApi2WithStartupClass.Api.IoC.Unity
{
    public static class UnityInitializer
    {
        public static void Initialize(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // Registers          
            
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}