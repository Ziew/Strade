using System.Linq;
using System.Web.Mvc;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using StockTrader;
using StockTrader.Models;
using StockTraderMongoService.Entities;
using StockTraderMongoService.Services;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof (UnityWebActivator), "Start")]
[assembly: ApplicationShutdownMethod(typeof (UnityWebActivator), "Shutdown")]

namespace StockTrader
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            IUnityContainer container = UnityConfig.GetConfiguredContainer();
            container.RegisterType<IEntityService<Company>, CompanyService>();
            container.RegisterType<IEntityService<TransactionHistory>, TransactionHistoryService>();


            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            //container.RegisterInstance<UserManager<ApplicationUser>>(ApplicationUserManager.Create());
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>();
            container.RegisterType<RoleManager<IdentityRole>, ApplicationRoleManager>();
            container.RegisterInstance<IdentityContext>(ApplicationIdentityContext.Create());

            //container.RegisterInstance<ApplicationUserManager>(ApplicationUserManager.Create());
            //container.RegisterType<IFinancialData,FinancialData>();
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            IUnityContainer container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}