using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Jly.Utility.Models;
using Jly.Utility.Util;

namespace Jly.Utility.Http
{
    public static class LoginHttp
    {
        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static async Task<User> LoginAsync(string name, string pwd)
        {

            var result = await API.HttpServiceUrl
                                .AppendPathSegment(API.Account_Login)
                                .SetQueryParams(new
                                {
                                    phone = name,
                                    passwd = pwd,
                                })
                                .GetAsync()
                                .ReceiveJson<JsonResult<User>>();

            if (result.Code != 0)
                throw new Exception(result.Message);

            return result.Data;
        }
    }
}
