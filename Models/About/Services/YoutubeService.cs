using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.About.Data;

namespace Dashboard.Models.About.Services
{
    public class YoutubeService : IService
    {
        public WidgetData GetChannelWidget()
        {
            WidgetData res = new WidgetData
            {
                Descrition = "Display a youtube channel with his number of subscribers",
                Name = "channel",
                Params = new List<ParamData>
                {
                    new ParamData
                    {
                        Name = "channelId",
                        Type = "string"
                    }
                }
            };
            return res;
        }

        public WidgetData GetVideoWidget()
        {
            WidgetData res = new WidgetData
            {
                Descrition = "Display a youtube video with his number of likes, dislikes, views and the video",
                Name = "video",
                Params = new List<ParamData>
                {
                    new ParamData
                    {
                        Name = "videoId",
                        Type = "string"
                    }
                }
            };
            return res;
        }

        public ServiceData GetService()
        {
            ServiceData res = new ServiceData
            {
                Name = "youtube",
                Widgets = new List<WidgetData>
                {
                    GetVideoWidget(),
                    GetChannelWidget()
                }
            };
            return res;
        }
    }
}
