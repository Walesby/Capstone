using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public enum RequestStatus
    {
        sent,accepted,declined
    }
    public class FriendList
    {
        public User User { get; set; }
        public Guid? UserId { get; set; }

        public User Friend { get; set; }
        public Guid? FriendId { get; set; }

        public DateTime RequestSent { get; set; }
        public RequestStatus RequestStatus { get; set; }

    }
}
