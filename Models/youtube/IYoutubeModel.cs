using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    interface IYoutubeModel
    {
        public VideoYoutube GetVideo(string url);
    }
}
