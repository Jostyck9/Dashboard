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

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IYoutubeModel YtModel = new YoutubeModel();
        private readonly IWeatherModel WModel = new WeatherModel();
        private readonly ISteamModel SModel = new SteamModel();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<IWidget> res = new List<IWidget>();
            if (HttpContext.User.Identity.IsAuthenticated) {
                res.Add(new WidgetVideoYoutube { Data = await YtModel.GetVideoById("wZZ7oFKsKzY") });
                res.Add(new WidgetChannelYoutube { Data = await YtModel.GetChannelById("UCYGjxo5ifuhnmvhPvCc3DJQ") });
                res.Add(new WidgetWeather { Data = await WModel.GetWeatherByLocation("London") });
                res.Add(new WidgetNewsSteam { Data = await SModel.GetLastNews("440", 2) });
                res.Add(new WidgetPlayersGameSteam { Data = new GameData { data = await SModel.GetCurrentPlayersGame("440"), Banner = SModel.GetGameBanner("440") } });
                res.Add(new WidgetAchievementsSteam { Data = await SModel.GetAchievementGame("440") });
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

        [HttpGet, Authorize]
        public ActionResult Data()
        {
            Dal test = new Dal();
            DataTest toAdd = test.getData();

            return View("_Data", toAdd);
        }
    }
}
