using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.About.Data
{
    public class ServerData
    {
        public long Current_time { get; set; }
        public List<ServiceData> Services { get; set; }
    }
}
