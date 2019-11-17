using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.About
{
    public interface IAbout
    {
        public string GetAboutData(string ipClient);
    }
}
