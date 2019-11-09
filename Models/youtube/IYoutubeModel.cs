using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    interface IYoutubeModel
    {
        public Task<VideoYoutube> GetVideoByUrl(string url);
        public Task<VideoYoutube> GetVideoById(string id);
        public Task<List<VideoYoutube>> SearchVideos(string query, int maxRes = 50);
        public Task<List<ChannelYoutube>> SearchChannels(string query, int maxRes = 20);
        public Task<ChannelYoutube> GetChannelById(string id);
        //public ChannelYoutube GetChannelByUsername(string username);
        //public ChannelYoutube GetChannelByUrl(string url);
    }
}
