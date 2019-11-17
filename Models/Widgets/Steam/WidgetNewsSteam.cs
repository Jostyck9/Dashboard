using Dashboard.Models.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets.Steam
{
    public class WidgetNewsSteam : IWidget
    {
        public AppNewsData Data { get; set; }
        public int IdWidget { get; set; }

        public int GetId()
        {
            return IdWidget;
        }
        WidgetsId IWidget.GetType()
        {
            return WidgetsId.STEAM_NEWS;
        }
    }
}
