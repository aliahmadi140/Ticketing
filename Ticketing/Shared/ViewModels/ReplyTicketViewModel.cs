using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Shared.ViewModels
{
    public class ReplyTicketViewModel
    {
        [Required]
        public int TicketId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Display(Name = "متن پاسخ")]
        public string ReplyText { get; set; }
    }
}
