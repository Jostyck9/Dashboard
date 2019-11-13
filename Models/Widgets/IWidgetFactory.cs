using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets
{
    interface IWidgetFactory
    {
        public Task<IWidget> CreateWidget(WidgetsSettings.Data.WidgetSetting data);
        public Task<List<IWidget>> CreateListWidget(List<WidgetsSettings.Data.WidgetSetting> data);
    }
}
