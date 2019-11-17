using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    interface IYoutubeModel
    {
        /**
        * @brief get video information from an url which is a string
        *
        * @return a structure of informations
        */
        public Task<VideoYoutube> GetVideoByUrl(string url);
        /**
        * @brief get video information from an id which is a string
        *
        * @return a structure of informations
        */
        public Task<VideoYoutube> GetVideoById(string id);
        /**
        * @brief search all video from a string with a max list
        *
        * @return a list of structure of informations
        */
        public Task<List<VideoYoutube>> SearchVideos(string query, int maxRes = 50);
        /**
        * @brief search all channels from a string with a max list
        *
        * @return a list of structure of informations
        */
        public Task<List<ChannelYoutube>> SearchChannels(string query, int maxRes = 20);
        /**
        * @brief get a channel by and id which is a string
        *
        * @return a structure of informations
        */
        public Task<ChannelYoutube> GetChannelById(string id);
    }
}
