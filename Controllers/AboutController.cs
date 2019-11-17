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
        
        /**
        * @brief get the local IP of the user
        * 
        * @return a string
        */
        private string GetLocalIp()
        {
            string ipaddress = string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList) {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    ipaddress = Convert.ToString(IP);
                    if (ipaddress.Contains("::ffff:")) {
                        ipaddress = ipaddress.Replace("::ffff:", "");
                    }
                }
            }
            return (ipaddress);
        }

        /*
        private string GetLocalIp()
        {
            string ipaddress = string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList) {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    ipaddress = Convert.ToString(IP);
                    if (ipaddress.Substring(0, 7) == "::ffff:") {
                        ipaddress = ipaddress.Substring(7);
                }
            }
            return (ipaddress);
        }*/

        [Route("about.json")]
        /**
        * @brief get all the elements to diplay in the about.json
        * 
        * @return a model
        */
        public string About()
        {
            var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if (ip.Contains("::ffff:")) {
                ip = ip.Replace("::ffff:", "");
            }
            if (ip == "::1") {
                ip = GetLocalIp();
            }
            /*var ip = _accessor.HttpContext?.Connection?.LocalIpAddress?.AddressFamily.ToString();*/
            /*var ip = Request.HttpContext?.Connection?.RemoteIpAddress.ToString();*/
            return _model.GetAboutData(ip);
        }
    }
}