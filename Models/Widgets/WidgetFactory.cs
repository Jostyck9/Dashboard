using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.Steam;
using Dashboard.Models.Weather;
using Dashboard.Models.Widgets.Steam;
using Dashboard.Models.Widgets.Weather;
using Dashboard.Models.Widgets.Youtube;
using Dashboard.Models.WidgetsSettings.Data;
using Dashboard.Models.youtube;

namespace Dashboard.Models.Widgets
{
    public class WidgetFactory : IWidgetFactory
    {
        private readonly IYoutubeModel _ytModel = new YoutubeModel();
        private readonly IWeatherModel _wModel = new WeatherModel();
        private readonly ISteamModel _sModel = new SteamModel();

        private async Task<IWidget> CreateYtChannelWidget(WidgetSetting data)
        {
            if (data.Params == null || data.Params.Length == 0)
                return null;
            return new WidgetChannelYoutube {Delay = data.TimerDelay, IdWidget = data.Id, Data = await _ytModel.GetChannelById(data.Params) };
        }

        private async Task<IWidget> CreateYtVideoWidget(WidgetSetting data)
        {
            if (data.Params == null || data.Params.Length == 0)
                return null;
            return new WidgetVideoYoutube { Delay = data.TimerDelay, IdWidget = data.Id, Data = await _ytModel.GetVideoById(data.Params) };
        }
        private async Task<IWidget> CreateWeatherWidget(WidgetSetting data)
        {
            float latitude;
            float longitude;
            if (data.Params == null || data.Params.Length == 0)
                return null;
            var parameters = data.Params.Split(';');
            if (parameters.Length == 1)
                return new WidgetWeather { Delay = data.TimerDelay, IdWidget = data.Id, Data = await _wModel.GetWeatherByLocation(parameters[0]) };
            try {
                latitude = float.Parse(parameters[0], CultureInfo.InvariantCulture);
                longitude = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                }
            catch (Exception) {
                return null;
            }
            return new WidgetWeather { Delay = data.TimerDelay, IdWidget = data.Id, Data = await _wModel.GetWeatherByCoord(latitude, longitude) };
        }
        private async Task<IWidget> CreateSteamAchievementWidget(WidgetSetting data)
        {
            if (data.Params == null || data.Params.Length == 0)
                return null;
            return new WidgetAchievementsSteam { Delay = data.TimerDelay, IdWidget = data.Id, Data = await _sModel.GetAchievementGame(data.Params) };
        }
        private async Task<IWidget> CreateSteamNewsWidget(WidgetSetting data)
        {
            ulong count;

            if (data.Params == null || data.Params.Length == 0)
                return null;
            var parameters = data.Params.Split(';');
            if (parameters.Length == 1)
                return new WidgetNewsSteam { Delay = data.TimerDelay, IdWidget = data.Id, Data = await _sModel.GetLastNews(parameters[0]) };
            try
            {
                count = ulong.Parse(parameters[1]);
            }
            catch (Exception)
            {
                return null;
            }
            return new WidgetNewsSteam { Delay = data.TimerDelay, IdWidget = data.Id, Data = await _sModel.GetLastNews(parameters[0], count) };
        }

        private async Task<IWidget> CreatePlayersWidget(WidgetSetting data)
        {
            if (data.Params == null || data.Params.Length == 0)
                return null;
            return new WidgetPlayersGameSteam { Delay = data.TimerDelay, IdWidget = data.Id, Data = new GameData { data = await _sModel.GetCurrentPlayersGame(data.Params), Banner = _sModel.GetGameBanner(data.Params) } };
        }

        public async Task<IWidget> CreateWidget(WidgetSetting data)
        {
            switch (data.WidgetId)
            {
                case WidgetsId.STEAM_ACHIEVEMENTS:
                    return await CreateSteamAchievementWidget(data);

                case WidgetsId.STEAM_NEWS:
                    return await CreateSteamNewsWidget(data);

                case WidgetsId.STEAM_PLAYERS:
                    return await CreatePlayersWidget(data);

                case WidgetsId.WEATHER:
                    return await CreateWeatherWidget(data);

                case WidgetsId.YOUTUBE_CHANNEL_SUB:
                    return await CreateYtChannelWidget(data);

                case WidgetsId.YOUTUBE_VIDEO:
                    return await CreateYtVideoWidget(data);

                default:
                    break;
            }
            return null;

        }

        public async Task<List<IWidget>> CreateListWidget(List<WidgetSetting> data)
        {
            List<IWidget> res = new List<IWidget>();

            foreach (var currentData in data)
            {
                var currentWidget = await CreateWidget(currentData);
                if (currentWidget != null)
                    res.Add(currentWidget);
            }
            return res;
        }
    }
}
