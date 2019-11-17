using Dashboard.Models.youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets.Youtube
{
    public class WidgetVideoYoutube : IWidget
    {
        public VideoYoutube Data { get; set; }
        public int IdWidget { get; set; }
        public ulong Delay { get; set; }

        public int GetId()
        {
            return IdWidget;
        }

        public ulong GetDelay()
        {
            return Delay;
        }

        WidgetsId IWidget.GetType()
        {
            return WidgetsId.YOUTUBE_VIDEO;
        }
    }
}
