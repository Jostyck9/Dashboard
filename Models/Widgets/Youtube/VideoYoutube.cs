using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    public class VideoYoutube
    {
        public string VideoUrl { get; set; }
        public string VideoId { get; set; }
        public string Title { get; set; }
        public ulong? ViewCount { get; set; }
        public ulong? Dislikes { get; set; }
        public ulong? Likes { get; set; }
    }
}
