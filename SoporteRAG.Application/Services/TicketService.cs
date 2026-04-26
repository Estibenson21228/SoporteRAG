using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Domain.Entities;

namespace SoporteRAG.Application.Services
{
    public class TicketService:ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEmbeddingService _embeddingService;
        private readonly IEmbeddingRepository _embeddingRepository;

        public TicketService(ITicketRepository ticketRepository, IEmbeddingService embeddingService, IEmbeddingRepository embeddingRepository)
        {
            _ticketRepository = ticketRepository;
            _embeddingService = embeddingService;
            _embeddingRepository = embeddingRepository;
        }

        public async Task<int> GetTotalTicketsAsync()
        {
            return await _ticketRepository.GetCountAsync();
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            ticket.FechaCreacion = DateTime.UtcNow;

            await _ticketRepository.AddAsync(ticket);

            var textToEmbed = $"""
                TicketId: {ticket.TicketId}
                Problema: {ticket.Problema}
                Solucion: {ticket.Solucion}
                """;

            var embeddingJson = await _embeddingService.GenerateEmbeddingAsync(textToEmbed);

            var embedding = new Embedding
            {
                TipoRecurso = "Ticket",
                RecursoId = ticket.Id,
                EmbeddingVector = embeddingJson,
                TextoOriginal = textToEmbed,
                FechaCreacion = DateTime.UtcNow
            };

            await _embeddingRepository.AddAsync(embedding);
        }


    }
}
