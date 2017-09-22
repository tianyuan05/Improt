using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    /// <summary>
    /// 乐园会员详细信息
    /// </summary>
    public class MemberDetailInfo
    {
        /// <summary>
        /// 用户Id opark_user
        /// </summary>
        public long OparkUserId { get; set; }

        public long? FamilyUserId { get; set; }

        /// <summary>
        /// 会员Id opark_member表主键
        /// </summary>
        public long OparkMemberId { get; set; }

        /// <summary>
        /// 会员昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// 会员状态
        /// </summary>
        public int? State { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string MemberCardNumber { get; set; }

        /// <summary>
        ///会员手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        public int? FamilyRoleId { get; set; }

        /// <summary>
        /// 邀请人姓名
        /// </summary>
        public string Inviter { get; set; }


        /// <summary>
        /// 邀请新会员人数
        /// </summary>
        public int InvitedCount { get; set; }

        /// <summary>
        /// 会员/新邀请标记
        /// <para>true-是会员</para>
        /// <para>false-不是会员</para>
        /// </summary>
        public bool? IsMember { get; set; }

        /// <summary>
        /// 转化状态
        /// 未用 = 1, 已用 = 10, 转化 = 20
        /// </summary>
        public int InviterState { get; set; }

        /// <summary>
        /// 会员剩余游戏币数
        /// </summary>
        public int? CoinCount { get; set; }

        /// <summary>
        /// 会员储值卡余额
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// 是否领取实体卡
        /// </summary>
        public int? Received { get; set; }

        public DateTime? CreateTime { get; set; }

    }
}
