using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
 * Ran out of time to implement
 */
namespace Capstone.Models
{
    public class Message
    {
        public User FromUser { get; set; }
        public Guid? FromUserId { get; set; }

        public User ToUser { get; set; }
        public Guid? ToUserId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }

    }
}
