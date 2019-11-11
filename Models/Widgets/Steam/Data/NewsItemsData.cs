using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Steam
{
    public class NewsItemsData
    {
        public ulong AppId { get; set; }
        public List<NewsData> NewsItems { get; set; }
    }
}
