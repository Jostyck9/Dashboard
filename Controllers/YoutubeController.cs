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

        public IActionResult Index(string query = "Pokemon")
        {
            return View(youtubeModel.SearchVideos(query));
        }

        [HttpGet("[controller]/[action]/{query?}")]
        public IActionResult Channels(string query = "")
        {
            var res = youtubeModel.SearchChannels(query);
            return View(res);
        }
    }
}