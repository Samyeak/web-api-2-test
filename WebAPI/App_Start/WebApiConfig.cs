using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using WebAPI.Controllers;
using WebAPI.Core;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //Configure IoC
            //config.ConfigureIoC();

            IUnityContainer container = new UnityContainer().AddExtension(new Diagnostic());
            //container.RegisterType<IGenericRepository<Product>, GenericRepository<Product>>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductsRepository, ProductsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductsController, ProductsController>(new HierarchicalLifetimeManager());
            container.RegisterType<EfContext>(new InjectionFactory(c => new EfContext()));
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            

        }

        private static void ConfigureIoC(this HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IGenericRepository<Product>, GenericRepository<Product>>(new HierarchicalLifetimeManager());
            //RegisterGenericRepository<Product>();
            config.DependencyResolver = new UnityResolver(container);

            void RegisterGenericRepository<T>() where T : class
            {
                container.RegisterType<IGenericRepository<T>, GenericRepository<T>>(new HierarchicalLifetimeManager());
            }
        }
    }
}
