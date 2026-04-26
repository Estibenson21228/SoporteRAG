using Microsoft.AspNetCore.Mvc;
using SoporteRAG.API.DTOs;
using SoporteRAG.Application.Services;
using SoporteRAG.Domain.Entities;

namespace SoporteRAG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketRequest request)
        {
            var ticket = new Ticket
            {
                TicketId=request.TicketId,
                Titulo=request.Titulo,
                Problema=request.Problema,
                Solucion=request.Solucion,
                Categoria=request.Categoria,
                Fecha=request.Fecha
            };
            await _ticketService.CreateTicketAsync(ticket);

            return Ok(new {Message="Ticket creado correctamente"});

        }
    }
}
