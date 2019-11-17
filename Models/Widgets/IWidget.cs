using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets
{
    public interface IWidget
    {
        public WidgetsId GetType();
        public int GetId();
    }

    public enum WidgetsId
    {
        WEATHER,
        YOUTUBE_CHANNEL_SUB,
        YOUTUBE_VIDEO,
        STEAM_ACHIEVEMENTS,
        STEAM_PLAYERS,
        STEAM_NEWS
    }
}
