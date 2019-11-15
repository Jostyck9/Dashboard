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

namespace Dashboard.Controllers
{
    public class EditController : Controller
    {
        private IDal _widgetsSettings;
        private readonly IYoutubeModel _ytModel;

        public EditController(ApplicationDbContext db)
        {
            _widgetsSettings = new Dal(db);
            _ytModel = new YoutubeModel();
        }

        [HttpGet, Route("Edit")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> AddChannelYoutube(string url)
        {
            return true;
        }

        [HttpPost, Authorize]
        public async Task<bool> AddVideoYoutube(string url)
        {
            VideoYoutube res = await _ytModel.GetVideoByUrl(url);

            if (res.VideoId == "")
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
    }
}