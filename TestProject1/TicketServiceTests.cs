using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Server.Contracts;
using Ticketing.Server.Data;
using Ticketing.Server.Services;
using Xunit;

namespace TestProject1
{
    public class TicketServiceTests
    {
        private readonly Mock<ApplicationDbContext> _context;
        private readonly ITicketService _ticketService;

        public TicketServiceTests()
        {
            _context = new Mock<ApplicationDbContext>();

            _ticketService = new TicketService(_context.Object);
        }

        [Fact]
        public async Task When_GetMyTicketsCalled_Then_ResultShouldNotBeNull()
        {
            //Arrange
            var userId = Guid.NewGuid().ToString();


            //Act
            var result = _ticketService.GetMyTickets(userId);


            //Assert
            Assert.NotNull(result);


        }


    }
}
