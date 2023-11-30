using System.Collections.Generic;
using System.Threading.Tasks;
using Ticketing.Shared.ViewModels;

namespace Ticketing.Server.Contracts
{
    public interface ITicketService
    {

        Task<List<TicketViewModel>> GetMyTickets(string userId);
    }
}
