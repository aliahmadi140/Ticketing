using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing.Server.Models;
using Ticketing.Shared.ViewModels;

namespace Ticketing.Server.AutoMapperProfiles
{
    public class TicketProfile:Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketViewModel>().ReverseMap();
        }
    }
}
