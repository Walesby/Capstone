using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Role : IdentityRole<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        override public Guid Id { get; set; }
    }
}
