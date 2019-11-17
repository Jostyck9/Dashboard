using Dashboard.Models.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets.Steam
{
    public class WidgetPlayersGameSteam : IWidget
    {
        public GameData Data { get; set; }
        public ulong Delay { get; set; }
        public int IdWidget { get; set; }

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
            return WidgetsId.STEAM_PLAYERS;
        }
    }
}
