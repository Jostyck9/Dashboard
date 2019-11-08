using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    interface IYoutubeModel
    {
        public VideoYoutube GetVideoByUrl(string url);
        public VideoYoutube GetVideoById(string id);
        public List<VideoYoutube> SearchVideos(string query, int maxRes = 50);

        public List<ChannelYoutube> SearchChannels(string query, int maxRes = 20);

        //public ChannelYoutube GetChannelByUrl(string url);
        public ChannelYoutube GetChannelById(string id);
        //public ChannelYoutube GetChannelByUsername(string username);
    }
}
