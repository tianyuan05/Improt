using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Models
{
    /// <summary>
    /// the entity be used member list 
    /// </summary>
    public class Member
    {
        public long Id { get; set; }

        public long? OparkUserId { get; set; }

        public long? OparkId { get; set; }

        public string CardNumber { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int? Gender { get; set; }

        public int? State { get; set; }

        public DateTime? CTime { get; set; }

        public string OpenId { get; set; }

        public string Memo { get; set; }
    }
}
