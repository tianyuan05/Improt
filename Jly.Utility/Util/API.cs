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

        #region Login

        /// <summary>
        /// Login API path
        /// </summary>
        public const string Account_Login = "/api/OparkAdmin/login";

        #endregion

        #region Members

        /// <summary>
        /// query member simple info list
        /// <para>0-current login opark id</para>
        /// </summary>
        public const string Member_QuerySimpleList = "/api/Member/{0}/QuerySmiple";

        /// <summary>
        /// query member detail info
        /// <para>0-current login opark id</para>
        /// </summary>
        public const string Member_QueryDetail = "/api/Member/{0}/QueryDetail";

        /// <summary>
        /// query member list
        /// <para>0-乐园Id</para>
        /// </summary>
        public const string Member_QueryList = "/api/Member/{0}/QueryList";

        #endregion

    }
}
