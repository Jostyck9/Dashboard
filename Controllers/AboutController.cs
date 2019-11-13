using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using System.Net.Sockets;
using Dashboard.Models.About;

namespace Dashboard.Controllers
{
    public class AboutController : Controller
    {
        IHttpContextAccessor _accessor;
        IAbout _model = new About();

        public AboutController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private string GetLocalIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }

        [Route("about.json")]
        public string About()
        {
            var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if (ip == "::1")
            {
                ip = GetLocalIp();
            }
            /*var ip = _accessor.HttpContext?.Connection?.LocalIpAddress?.AddressFamily.ToString();*/
            /*var ip = Request.HttpContext?.Connection?.RemoteIpAddress.ToString();*/
            return _model.GetAboutData(ip);
        }
    }
}