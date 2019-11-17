﻿using System;
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

        /**
        * @brief assign all Model to local variables in controller
        * 
        */
        public EditController(ApplicationDbContext db)
        {
            _widgetsSettings = new Dal(db);
            _ytModel = new YoutubeModel();
            _sModel = new SteamModel();
            _wModel = new WeatherModel();
        }

        [HttpGet, Route("Edit")]
        
        /**
        * @brief get the view of the EditController
        * 
        * @return a View
        */
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Authorize]
        /**
        * @brief Add a youtube channel from his id from a string
        * 
        * @return a bool
        */
        public async Task<bool> AddChannelYoutube(string id, ulong delay = 20)
        {
             ChannelYoutube res = await _ytModel.GetChannelById(id);

            if (res.Id == null || res.Id == "")
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.YOUTUBE_CHANNEL_SUB, res.Id, delay);
            }
            return true;
        }

        [HttpPost, Authorize]
        /**
        * @brief Add a youtube video from his url from a string
        * 
        * @return a bool
        */
        public async Task<bool> AddVideoYoutube(string url, ulong delay = 20)
        {
            VideoYoutube res = await _ytModel.GetVideoByUrl(url);

            if (res.VideoId == null || res.VideoId == "")
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.YOUTUBE_VIDEO, res.VideoId, delay);
            }
            return true;
        }

        [HttpPost, Authorize]
        /**
        * @brief add the weather widget from his location from a string
        * 
        * @return a bool
        */
        public async Task<bool> AddWeather(string location, ulong delay = 20)
        {
            WeatherData res = await _wModel.GetWeatherByLocation(location);

            if (res.Name == null || res.Name == "" || res.Main == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.WEATHER, res.Name, delay = 20);
            }
            return true;
        }

        [HttpPost, Authorize]
        /**
        * @brief add a Steam widget from his application Id from a string which display News from the game
        * 
        * @return a bool
        */
        public async Task<bool> AddNewsSteam(string appId, ulong delay = 20)
        {
            ResponseData res = await _sModel.GetCurrentPlayersGame(appId);

            if (res == null || res.Response == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.STEAM_NEWS, appId, delay = 20);
            }
            return true;
        }

        [HttpPost, Authorize]
        /**
        * @brief add a Steam widget from his application id from a string which display number of players
        * 
        * @return a bool
        */
        public async Task<bool> AddPlayersSteam(string appId, ulong delay = 20)
        {
            ResponseData res = await _sModel.GetCurrentPlayersGame(appId);

            if (res == null || res.Response == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.STEAM_PLAYERS, appId, delay);
            }
            return true;
        }

        [HttpPost, Authorize]
        /**
        * @brief add a Steam widget from his application id from a string which display the achivement of the game
        * 
        * @return a bool
        */
        public async Task<bool> AddAchievementSteam(string appId, ulong delay = 20)
        {
            ResponseData res = await _sModel.GetCurrentPlayersGame(appId);

            if (res == null || res.Response == null)
            {
                return false;
            }
            var currentUser = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != null)
            {
                _widgetsSettings.AddWidget(currentUser, WidgetsId.STEAM_ACHIEVEMENTS, appId, delay);
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