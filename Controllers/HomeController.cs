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
        }

        ~HomeController()
        {
            _widgetsSettings.Dispose();
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Widgets()
        {
            List<IWidget> res = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (currentUser != null)
                {
                    var userDatas = _widgetsSettings.GetWidgetsByUsr(currentUser);
                    res = await _factory.CreateListWidget(userDatas);
                }
            }
            return View("_columns", res);
        }

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
