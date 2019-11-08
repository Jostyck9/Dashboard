using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    public class ChannelYoutube
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Banner { get; set; }
        public ulong? SubCount {get; set;}
    }
}
