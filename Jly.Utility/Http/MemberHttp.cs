using Jly.Utility.Models;
using Jly.Utility.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Jly.Utility.Http
{
    public class MemberHttp
    {
        /// <summary>
        /// query list of member simple info
        /// </summary>
        /// <param name="oparkId">current login opark id</param>
        /// <returns>the list of member simple info<see cref="MemberSimpleInfo"/></returns>
        public static async Task<List<MemberSimpleInfo>> GetMemberSimpleListAsync()
        {
            var result = await API.HttpServiceUrl
                                .AppendPathSegment(string.Format(API.Member_QuerySimpleList, Session.Opark.Id))
                                .GetAsync()
                                .ReceiveJson<JsonResult<List<MemberSimpleInfo>>>();

            if (result.Code != 0)
                throw new Exception(result.Message);

            return result.Data;

        }

        /// <summary>
        /// query entity of member detail info
        /// </summary>
        /// <param name="condition">query condition key</param>
        /// <returns>the instance of member detail info <see cref="MemberDetailInfo"/></returns>
        public static async Task<MemberDetailInfo> GetMemberDetailAsync(string condition)
        {
            var result = await API.HttpServiceUrl
                                .AppendPathSegment(string.Format(API.Member_QueryDetail, Session.Opark.Id))
                                .SetQueryParam("key", condition)
                                .GetAsync()
                                .ReceiveJson<JsonResult<MemberDetailInfo>>();

            if (result.Code != 0)
                throw new Exception(result.Message);

            return result.Data;

        }

        /// <summary>
        /// query list of mmeber by paging
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="gender"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static async Task<Pager<Member>> GetMemberAsync(string name = null, string phone = null, int? gender = null, int index = 1, int size = 10)
        {
            var result = await API.HttpServiceUrl
                                .AppendPathSegment(string.Format(API.Member_QueryList, Session.Opark.Id))
                                .SetQueryParams(new
                                {
                                    phone = phone,
                                    name = name,
                                    gender = gender,
                                    pageNo = index,
                                    pageSize = size,
                                })
                                .GetAsync()
                                .ReceiveJson<JsonResult<Pager<Member>>>();

            if (result.Code != 0)
                throw new Exception(result.Message);

            return result.Data;


        }


    }
}
