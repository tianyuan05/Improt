using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Core
{
    public class FlurlHttpConfigure
    {
        public static void Configure()
        {
            Flurl.Http.FlurlHttp.Configure(c =>
            {
                //c.BeforeCall = BeforeCall; // Request之前处理 (添加Header)
                c.OnError = HandleError; //异常处理

            });
        }

        /// <summary>
        /// FlurlHttp异常处理
        /// </summary>
        /// <param name="call"></param>
        static void HandleError(Flurl.Http.HttpCall call)
        {
            Console.WriteLine($"HttpStatusCode:{call.HttpStatus}---{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            Console.WriteLine($"ExceptionMessage:{call.Exception.Message}");

            throw new Exception("网络异常");
            //System.Windows.MessageBox.Show("网络异常");
        }

        /// <summary>
        /// 调用Flurl接口之前执行的方法
        /// </summary>
        /// <param name="call"></param>
        static void BeforeCall(Flurl.Http.HttpCall call)
        {
            //if (call.Url.RemoveQueryParams("phone", "passwd") == $"{Apis.DotNetServiceUrl}{Apis.OparkAdmin_Login}") //登陆不添加Token
            //    return;

            //call.Request.Headers.Add("Authorization", $"Bearer{Session.User?.Token}"); //添加Token
        }
    }
}
