using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Ticketing.Shared.Utilities.Enums;

namespace Ticketing.Server.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }

        public int? ChildTicketId { get; set; }
        [ForeignKey("ChildTicketId")]
        public Ticket ChildTicket { get; set; }
    }
   
}
