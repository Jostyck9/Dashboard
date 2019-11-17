using Dashboard.Models.About.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.About
{
    interface IService
    {
        public ServiceData GetService(); 
    }
}
