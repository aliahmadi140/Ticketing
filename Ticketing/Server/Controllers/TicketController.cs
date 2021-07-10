using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing.Server.Data;
using Ticketing.Server.Models;
using Ticketing.Shared.Utilities;
using Ticketing.Shared.ViewModels;

namespace Ticketing.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TicketController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<Result> SendTicket([FromBody] TicketViewModel ticket)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    Ticket newTicket = _mapper.Map<Ticket>(ticket);
                    newTicket.UserId = userId;
                    newTicket.ExpireDate = DateTime.Now.AddHours(48);
                    await _context.Tickets.AddAsync(newTicket);
                    await _context.SaveChangesAsync();
                    return new Result() { Message = Constants.SuccessMessage, Status = Enums.Status.Success };
                }
                else
                {
                    return new Result() { Message = "خطا در هنگام دریافت اطلاعات کاربر", Status = Enums.Status.Failure };

                }

            }
            catch (Exception ex)
            {
                return new Result() { Status = Enums.Status.Failure, Message = ex.Message };
            }
        }

        [HttpGet]
        public async Task<GenericResult<List<TicketViewModel>>> GetMyTickets()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

                var result = await _context.Tickets.Where(x => x.UserId == userId &&x.TicketStatus!=Enums.TicketStatus.Reply ).Include(x => x.ChildTicket).ToListAsync();
                return new GenericResult<List<TicketViewModel>>()
                {
                    Model = result.Select(x => new TicketViewModel
                    {
                        UserId = x.UserId,
                        Text = x.Text,
                       ReplyText=x.ChildTicket!=null?x.ChildTicket.Text:null,
                        ExpireDate = x.ExpireDate,
                        TicketStatus = DateTime.Now > x.ExpireDate || x.ChildTicketId > 0 ? Enums.TicketStatus.Closed : Enums.TicketStatus.Pending,
                        Title = x.Title,
                        Id = x.Id
                    }).ToList(),
                    Status = Enums.Status.Success
                };
            }
            catch (Exception ex)
            {
                return new GenericResult<List<TicketViewModel>>() { Message = ex.Message, Status = Enums.Status.Failure };
            }
        }


        [HttpGet("{id}")]
        public async Task<GenericResult<TicketViewModel>> GetTicketInformation(int id)
        {
            try
            {
                var ticket = await _context.Tickets.Where(x => x.Id == id).Include(x => x.User).Include(x => x.ChildTicket).FirstOrDefaultAsync();
                return new GenericResult<TicketViewModel>()
                {
                    Model = new TicketViewModel
                    {
                        UserId = ticket.UserId,
                        Text = ticket.Text,
                        ReplyText = ticket.ChildTicket != null ? ticket.ChildTicket.Text : null,
                        UserName = ticket.User.UserName,
                        ExpireDate = ticket.ExpireDate,
                        TicketStatus = DateTime.Now > ticket.ExpireDate || ticket.ChildTicketId > 0 ? Enums.TicketStatus.Closed : Enums.TicketStatus.Pending,
                        Title = ticket.Title,
                        Id = ticket.Id
                    },
                    Status = Enums.Status.Success
                };

            }
            catch (Exception ex)
            {
                return new GenericResult<TicketViewModel>() { Message = ex.Message, Status = Enums.Status.Failure };
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<Result> SendReply([FromBody] ReplyTicketViewModel model)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

                var replyTicket = new Ticket { UserId = userId, Text = model.ReplyText,TicketStatus=Enums.TicketStatus.Reply,ExpireDate=DateTime.Now };
                await _context.Tickets.AddAsync(replyTicket);
                await _context.SaveChangesAsync();



                var currentTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == model.TicketId);
                currentTicket.TicketStatus = Enums.TicketStatus.Closed;
                currentTicket.ChildTicketId = replyTicket.Id;
                 _context.Tickets.Update(currentTicket);

                await _context.SaveChangesAsync();

                return new Result() { Message = "عملیات با موفقیت انجام شد", Status = Enums.Status.Success };
            }
            catch (Exception ex)
            {
                return new Result() { Message = ex.Message, Status = Enums.Status.Failure };
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<GenericResult<List<TicketViewModel>>> GetPendingTickets()
        {
            try
            {


                var result = await _context.Tickets.Where(x => x.TicketStatus == Enums.TicketStatus.Pending && x.ExpireDate > DateTime.Now).Include(x => x.ChildTicket).ToListAsync();
                return new GenericResult<List<TicketViewModel>>()
                {
                    Model = result.Select(x => new TicketViewModel
                    {
                        UserId = x.UserId,
                        Text = x.Text,
                        ExpireDate = x.ExpireDate,
                        Title = x.Title,
                        Id = x.Id,
                        TicketStatus = DateTime.Now > x.ExpireDate || x.ChildTicketId > 0 ? Enums.TicketStatus.Closed : Enums.TicketStatus.Pending,

                    }).ToList(),
                    Status = Enums.Status.Success,

                };
            }
            catch (Exception ex)
            {
                return new GenericResult<List<TicketViewModel>>() { Message = ex.Message, Status = Enums.Status.Failure };
            }
        }
    }
}
