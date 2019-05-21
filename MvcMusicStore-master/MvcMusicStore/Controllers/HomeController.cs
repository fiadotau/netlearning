using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Infrastructure;
using MvcMusicStore.Models;
using NLog;
using PerformanceCounterHelper;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private MusicStoreEntities storeDB = new MusicStoreEntities();

		private readonly ILogger logger;

		private static CounterHelper<Counters> counterHomePage;
		//
		// GET: /Home/

		public HomeController(ILogger logger)
		{
			counterHomePage = PerformanceHelper.CreateCounterHelper<Counters>("Home Page");
			this.logger = logger;
		}

		public ActionResult Index()
        {
			//logger.Debug("Enter to home page");
			logger.Log(LogLevel.Debug, "Enter to home page");
			counterHomePage.Increment(Counters.GoToHome);
			// Get most popular albums
			var albums = GetTopSellingAlbums(6);

            return View(albums);
        }


        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
    }
}