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
        /**
        * @brief get all widgets settings of all widgets
        *
        * @return a list of Widgets Settings
        */
        public List<WidgetSetting> GetAllWidgetsSettings();

        /**
        * @brief add a widget to a user with parameters
        */
        public void AddWidget(string userId, WidgetsId widgetId, string parameters);
        /**
        * @brief get all widgets from an user
        *
        * @return a list of Widgets Settings
        */
        public List<WidgetSetting> GetWidgetsByUsr(string userId);
        /**
        * @brief delete all widgets from a User Id for an user
        */
        public void DeleteWidgetsByUsr(string userId);

        /**
        * @brief delete a widget from a User Id and a widget id for an user
        */
        public void DeleteWidgetsById(string userId, int id);
        /**
        * @brief delete all widget for all users
        */
        public void DeleteAllWidgets();
        /**
        * @brief update all the parameters of the widget
        */
        public void UpdateWidgetParam(int widgetId, string parameters);
    }
}
