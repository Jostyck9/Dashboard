using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Steam
{
    public class NewsItemsData
    {
        public ulong AppId { get; set; }
        public IEnumerable<NewsData> NewsItems { get; set; }
    }
}
