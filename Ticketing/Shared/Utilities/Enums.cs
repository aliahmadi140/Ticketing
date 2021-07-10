using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Shared.Utilities
{
    public class Enums
    {
        public enum TicketStatus
        {
            [Display(Name = "در انتظار پاسخ")]
            Pending,
            [Display(Name = "بسته شده")]
            Closed,
            [Display(Name = "پاسخ")]
            Reply
        }

        public enum Status
        {
            Success,
            Failure
        }
    }
}
