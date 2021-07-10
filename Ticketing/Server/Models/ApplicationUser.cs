using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketing.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Ticket> Tickets { get; set; }
    }
}
