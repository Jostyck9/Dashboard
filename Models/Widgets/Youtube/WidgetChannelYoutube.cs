using Dashboard.Models.youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets.Youtube
{
    public class WidgetChannelYoutube : IWidget
    {
        public ChannelYoutube Data { get; set; }
        public int IdWidget { get; set; }
        public ulong Delay { get; set; }

        public ulong GetDelay()
        {
            return Delay;
        }

        public int GetId()
        {
            return IdWidget;
        }

        WidgetsId IWidget.GetType()
        {
            return WidgetsId.YOUTUBE_CHANNEL_SUB;
        }
    }
}
