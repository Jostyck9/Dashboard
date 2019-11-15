using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.Widgets;
using Dashboard.Models.WidgetsSettings.Data;

namespace Dashboard.Models.WidgetsSettings
{
    interface IDal : IDisposable
    {
        public List<WidgetSetting> GetAllWidgetsSettings();

        public void AddWidget(string userId, WidgetsId widgetId, string parameters);
        public List<WidgetSetting> GetWidgetsByUsr(string userId);
        public void DeleteWidgetsByUsr(string userId);
        /*public void DeleteWidgetsById(string userId, ulong id);*/
        public void DeleteAllWidgets();
        public void UpdateWidgetParam(int widgetId, string parameters);
    }
}
