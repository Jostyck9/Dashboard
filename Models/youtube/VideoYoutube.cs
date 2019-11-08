using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    public class VideoYoutube
    {
        public string VideoUrl { get; set; }
        public string Title { get; set; }
        public ulong? viewCount { get; set; }
        public ulong? dislikes { get; set; }
        public ulong? likes { get; set; }
    }
}
