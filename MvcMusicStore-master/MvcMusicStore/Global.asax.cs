using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using MvcMusicStore.Infrastructure;
using NLog;
using PerformanceCounterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcMusicStore
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		private readonly ILogger logger;

		public MvcApplication()
		{
			logger = LogManager.GetCurrentClassLogger();
		}

		protected void Application_Start()
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(HomeController).Assembly);
			builder.Register(f => LogManager.GetLogger("ForControllers")).As<ILogger>();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));


			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

			AppConfig.Configure();
			//logger.Log(LogLevel.Info, "Application started");
			logger.Info("Application started");
			using (var counterHelper = PerformanceHelper.CreateCounterHelper<Counters>("Test project"))
			{
				counterHelper.RawValue(Counters.GoToHome, 0);
			}
		}

		protected void Application_Error()
		{
			var ex = Server.GetLastError();
			logger.Log(LogLevel.Error, ex.ToString());
		}
	}
}