using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    public static class Session
    {
        /// <summary>
        /// current login opark id
        /// </summary>
        public static string OparkId { get; set; }

        /// <summary>
        /// current login opark
        /// </summary>
        public static Opark Opark { get; set; }

        /// <summary>
        /// current login user info
        /// </summary>
        public static UserInfo User { get; set; }


    }
}
