using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap;
using VTP2015.DataAccess;
using VTP2015.Identity;
using VTP2015.Infrastructure;
using VTP2015.Infrastructure.Tasks;
using VTP2015.Infrastructure.ViewEngine;

namespace VTP2015
{
    public class MvcApplication : HttpApplication
    {

        public IContainer Container
        {
            get { return (IContainer) HttpContext.Current.Items["_Container"]; }
            set { HttpContext.Current.Items["_Container"] = value; }
        }

        protected void Application_Start()
        {
            Container = ContainerFactory.Container;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomViewEngine());

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container));

            foreach (var task in Container.GetAllInstances<IRunAtInit>())
            {
                task.Execute();
            }
            foreach (var task in Container.GetAllInstances<IRunAtStartup>())
            {
                task.Execute();
            }

            Database.SetInitializer<Context>(null);
            Database.SetInitializer<ApplicationDbContext>(null);

            
        }

        public void Application_BeginRequest()
        {
            Container = ContainerFactory.Container.GetNestedContainer();

            foreach (var task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }


        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;  
            }
        }

    }
}
