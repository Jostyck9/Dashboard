using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dashboard.Data;
using Dashboard.Models.Widgets;
using Dashboard.Models.WidgetsSettings.Data;
using Microsoft.Extensions.Logging;

namespace Dashboard.Models.WidgetsSettings
{
    public class Dal : IDal
    {
        private readonly ApplicationDbContext _db;

        public Dal(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            /*db.Dispose();*/
        }

        public List<WidgetSetting> GetAllWidgetsSettings()
        {
            return (_db.WidgetsSettings.ToList());

        }

        public void AddWidget(string userId, WidgetsId widgetId, string parameters)
        {
            _db.WidgetsSettings.Add(new WidgetSetting { UserId = userId, WidgetId = widgetId, Params = parameters });
            _db.SaveChanges();
        }

        public List<WidgetSetting> GetWidgetsByUsr(string userId)
        {
            var res = _db.WidgetsSettings.Where(s => s.UserId == userId);
            return res.ToList();
        }

        public void DeleteWidgetsByUsr(string userId)
        {
            var listWidgets = GetWidgetsByUsr(userId);

            foreach (var widget in listWidgets)
            {
                _db.WidgetsSettings.Remove(widget);
            }
            _db.SaveChanges();
        }

        public void UpdateWidgetParam(int widgetId, string parameters)
        {
            var widget = _db.WidgetsSettings.FirstOrDefault(w => w.Id == widgetId);
            if (widget != null)
            {
                widget.Params = parameters;
            }
            _db.SaveChanges();
        }

        public void DeleteAllWidgets()
        {
            _db.WidgetsSettings.RemoveRange(_db.WidgetsSettings.ToArray());
            _db.SaveChanges();
        }

        public void DeleteWidgetsById(string userId, int id)
        {
            var listWidgets = GetWidgetsByUsr(userId);

            foreach (var widget in listWidgets)
            {
                if (widget.Id == id)
                {
                    _db.WidgetsSettings.Remove(widget);
                    break;
                }
            }
            _db.SaveChanges();
        }
    }
}
