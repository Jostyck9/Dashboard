using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Dashboard.Models.Widgets;
using Dashboard.Models.Widgets.Youtube;
using Dashboard.Models.youtube;
using Dashboard.Models.Weather;
using Dashboard.Models.Widgets.Weather;
using Dashboard.Models.Steam;
using Dashboard.Models.Widgets.Steam;
using System.Security.Claims;
using Dashboard.Models.WidgetsSettings;
using Dashboard.Data;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWidgetFactory _factory = new WidgetFactory();
        private IDal _widgetsSettings;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _widgetsSettings = new Dal(db);
            _logger = logger;

            /**
             * 
             * Test for the db and the configured widgets
             * 
             */
            /*_widgetsSettings.AddWidget("56a72f73-eb1e-4874-9efd-20b73f129626", WidgetsId.STEAM_PLAYERS, "440");
            _widgetsSettings.AddWidget("56a72f73-eb1e-4874-9efd-20b73f129626", WidgetsId.STEAM_PLAYERS, "1085660");
            _widgetsSettings.AddWidget("56a72f73-eb1e-4874-9efd-20b73f129626", WidgetsId.WEATHER, "Nantes");
            _widgetsSettings.AddWidget("56a72f73-eb1e-4874-9efd-20b73f129626", WidgetsId.YOUTUBE_CHANNEL_SUB, "UCYGjxo5ifuhnmvhPvCc3DJQ");
            _widgetsSettings.AddWidget("56a72f73-eb1e-4874-9efd-20b73f129626", WidgetsId.YOUTUBE_VIDEO, "wZZ7oFKsKzY");
            _widgetsSettings.AddWidget("56a72f73-eb1e-4874-9efd-20b73f129626", WidgetsId.YOUTUBE_VIDEO, "KaqC5FnvAEc");

            _widgetsSettings.AddWidget("319001e4-041f-44ae-a412-e452e6133eb3", WidgetsId.STEAM_NEWS, "440;2");
            _widgetsSettings.AddWidget("319001e4-041f-44ae-a412-e452e6133eb3", WidgetsId.STEAM_PLAYERS, "440");
            _widgetsSettings.AddWidget("319001e4-041f-44ae-a412-e452e6133eb3", WidgetsId.WEATHER, "Paris");*/
        }

        ~HomeController()
        {
            _widgetsSettings.Dispose();
        }

        /**
        * @brief check if the user is authenticated or not and display his saved widgets
        *
        * @return a View
        */
        public async Task<IActionResult> Index()
        {
            List<IWidget> res = null;

            if (HttpContext.User.Identity.IsAuthenticated) {
                var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUser != null)
                {
                    var userDatas = _widgetsSettings.GetWidgetsByUsr(currentUser);
                    res = await _factory.CreateListWidget(userDatas);
                }
            }
            return View(res);
        }

        /**
        * @brief return the View of the privacy
        *
        * @return a view
        */
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        /**
        * @brief return the View of the error
        *
        * @return a view
        */
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
