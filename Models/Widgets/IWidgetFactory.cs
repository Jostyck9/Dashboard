using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets
{
    interface IWidgetFactory
    {
        /**
        * @brief create a Widget
        *
        * @return a Task of IWdiget
        */
        public Task<IWidget> CreateWidget(WidgetsSettings.Data.WidgetSetting data);
        /**
        * @brief create a list of widget
        *
        * @return a Task of List of IWdiget
        */
        public Task<List<IWidget>> CreateListWidget(List<WidgetsSettings.Data.WidgetSetting> data);
    }
}
