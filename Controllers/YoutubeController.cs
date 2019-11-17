using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.youtube;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class YoutubeController : Controller
    {
        private IYoutubeModel youtubeModel = new YoutubeModel();

        /**
        * @brief Search for a youtube video from string
        * 
        * @return a View
        */
        public async Task<IActionResult> Index(string query = "Pokemon")
        {
            return View(await youtubeModel.SearchVideos(query));
        }

        [HttpGet("[controller]/[action]/{query?}")]
        /**
        * @brief Search for a youtube channel from string
        * 
        * @return a View
        */
        public async Task<IActionResult> Channels(string query = "")
        {
            var res = await youtubeModel.SearchChannels(query);
            return View(res);
        }
    }
}