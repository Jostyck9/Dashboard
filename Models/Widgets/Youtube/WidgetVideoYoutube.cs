﻿using Dashboard.Models.youtube;
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

        public int GetId()
        {
            return IdWidget;
        }

        WidgetsId IWidget.GetType()
        {
            return WidgetsId.YOUTUBE_VIDEO;
        }
    }
}
