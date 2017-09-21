using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    /// <summary>
    /// 会员原始信息类
    /// </summary>
    public class OriginalMemberInfo
    {
        /// <summary>
        /// 会员名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 会员手机号 opark_user 表phone唯一索引，
        /// 当phone已经存在与opark_user表中则去opark_user表中的Id存在opark_member表中的opark_user_id字段
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 套餐剩余量
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// 用户性别 opark_user 表 gender NULL=0,1-男,2-女
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 会员加入时间 opark_member表ctime
        /// </summary>
        public string CTime { get; set; }

        /// <summary>
        /// 套餐有效期至 时间
        /// </summary>
        public string DeadlineTime { get; set; }

    }
}
