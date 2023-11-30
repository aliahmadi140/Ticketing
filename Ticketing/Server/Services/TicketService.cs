using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing.Server.Contracts;
using Ticketing.Server.Data;
using Ticketing.Server.Models;
using Ticketing.Shared.Utilities;
using Ticketing.Shared.ViewModels;

namespace Ticketing.Server.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketViewModel>> GetMyTickets(string userId)
        {
            List<Ticket> tickets = await GetMyTicketFromDb(userId);

            List<TicketViewModel> result = ConvertTicketsToTicketViewModel(tickets);

            return result;
        }

        private static List<TicketViewModel> ConvertTicketsToTicketViewModel(List<Ticket> tickets)
        {
            return tickets.Select(x => new TicketViewModel
            {
                UserId = x.UserId,
                Text = x.Text,
                ReplyText = x.ChildTicket != null ? x.ChildTicket.Text : null,
                ExpireDate = x.ExpireDate,
                TicketStatus = DateTime.Now > x.ExpireDate || x.ChildTicketId > 0 ? Enums.TicketStatus.Closed : Enums.TicketStatus.Pending,
                Title = x.Title,
                Id = x.Id
            }).ToList();
        }

        private async Task<List<Ticket>> GetMyTicketFromDb(string userId)
        {
            return await _context.Tickets
                .Where(x => x.UserId == userId && x.TicketStatus != Enums.TicketStatus.Reply)
                .Include(x => x.ChildTicket).ToListAsync();
        }
    }
}
