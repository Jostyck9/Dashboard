using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.youtube;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Dashboard.Data;
using Dashboard.Models.WidgetsSettings;
using Dashboard.Models.Widgets;
using Dashboard.Models.Weather;
using Dashboard.Models.Steam;

namespace Dashboard.Controllers
{
    public class EditController : Controller
    {
        private IDal _widgetsSettings;
        private readonly IYoutubeModel _ytModel;
        private readonly IWeatherModel _wModel;
        private readonly ISteamModel _sModel;

        public EditController(ApplicationDbContext db)
        {
            _widgetsSettings = new Dal(db);
            _ytModel = new YoutubeModel();
            _sModel = new SteamModel();
            _wModel = new WeatherModel();
        }

        [HttpGet, Route("Edit")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Authorize]
        public async Task<bool> AddChannelYoutube(string id)
        {
             ChannelYoutube res = await _ytModel.GetChannelById(id);

            if (res.Id == null || res.Id == "")
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.YOUTUBE_CHANNEL_SUB, res.Id);
            }
            return true;
        }

        [HttpPost, Authorize]
        public async Task<bool> AddVideoYoutube(string url)
        {
            VideoYoutube res = await _ytModel.GetVideoByUrl(url);

            if (res.VideoId == null || res.VideoId == "")
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.YOUTUBE_VIDEO, res.VideoId);
            }
            return true;
        }

        [HttpPost, Authorize]
        public async Task<bool> AddWeather(string location)
        {
            WeatherData res = await _wModel.GetWeatherByLocation(location);

            if (res.Name == null || res.Name == "" || res.Main == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.WEATHER, res.Name);
            }
            return true;
        }

        [HttpPost, Authorize]
        public async Task<bool> AddNewsSteam(string appId)
        {
            ResponseData res = await _sModel.GetCurrentPlayersGame(appId);

            if (res == null || res.Response == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.STEAM_NEWS, appId);
            }
            return true;
        }

        [HttpPost, Authorize]
        public async Task<bool> AddPlayersSteam(string appId)
        {
            ResponseData res = await _sModel.GetCurrentPlayersGame(appId);

            if (res == null || res.Response == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.STEAM_PLAYERS, appId);
            }
            return true;
        }

        [HttpPost, Authorize]
        public async Task<bool> AddAchievementSteam(string appId)
        {
            ResponseData res = await _sModel.GetCurrentPlayersGame(appId);

            if (res == null || res.Response == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.STEAM_ACHIEVEMENTS, appId);
            }
            return true;
        }

        [HttpPost, Authorize]
        public bool DeleteWidget(int id)
        {
            if (id <= 0)
                return false;
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.DeleteWidgetsById(currentUser, id);
            }
            return true;
        }
    }
}