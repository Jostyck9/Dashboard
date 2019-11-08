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
            /*List<VideoYoutube> res = new List<VideoYoutube>
            {
                youtubeModel.GetVideoByUrl("https://www.youtube.com/watch?v=xv7MziaiN44")
            };*/
            return View(youtubeModel.SearchVideos(query));
        }
    }
}