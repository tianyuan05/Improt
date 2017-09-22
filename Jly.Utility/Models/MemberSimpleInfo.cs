using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    /// <summary>
    /// 乐园会员的简洁信息
    /// </summary>
    public class MemberSimpleInfo
    {
        /// <summary>
        /// opark_user表主键
        /// </summary>
        public long? OparkUserId { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 会员手机号
        /// </summary>
        public string Phone { get; set; }
    }
}
