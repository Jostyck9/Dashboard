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
        public IActionResult Index()
        {
            var res = YoutubeModel.getVideo();
            return View(res);
        }
    }
}