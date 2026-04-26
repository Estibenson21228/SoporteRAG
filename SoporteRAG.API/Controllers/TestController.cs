using Microsoft.AspNetCore.Mvc;
using SoporteRAG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Application.Services;

namespace SoporteRAG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        
        public TestController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("tickets-count")]
        public async Task<IActionResult> GetTicketCount()
        {
            var count = await _ticketService.GetTotalTicketsAsync();
            return Ok(new { TotalTickets = count });
        }


    }
}
