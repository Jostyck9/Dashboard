using System.ComponentModel.DataAnnotations;
using Dashboard.Models.Widgets;
using System.Collections.Generic;

namespace Dashboard.Models.WidgetsSettings.Data
{
    public class WidgetSetting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public WidgetsId WidgetId { get; set; }
        [Required]
        public string Params { get; set; }
    }
}
