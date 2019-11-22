using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public enum RequestStatus
    {
        waiting, added
    }
    public class Friend
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public DateTime RequestSent { get; set; }
        public RequestStatus RequestStatus{ get; set; }

        public virtual User User { get; set; }
    }
}
