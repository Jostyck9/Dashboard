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
        public IActionResult Index()
        {
            List<VideoYoutube> res = new List<VideoYoutube>
            {
                youtubeModel.GetVideo("https://www.youtube.com/watch?v=xv7MziaiN44")
            };
            return View(res);
        }
    }
}