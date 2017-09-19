using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Util
{
    /// <summary>
    /// Rest API url
    /// </summary>
    public static class API
    {
        /// <summary>
        /// Rest API host
        /// </summary>
        public static string HttpServiceUrl { get; private set; } = ConfigurationManager.AppSettings["HttpServiceUrl"];

        /// <summary>
        /// Login API path
        /// </summary>
        public const string Account_Login = "/api/OparkAdmin/login";

    }
}
