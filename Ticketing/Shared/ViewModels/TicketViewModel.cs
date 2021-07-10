using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ticketing.Shared.Utilities.Enums;

namespace Ticketing.Shared.ViewModels
{
   public class TicketViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string ReplyText { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }


    }
}
