

namespace SoporteRAG.API.DTOs
{
    public class CreateTicketRequest
    {
        public string TicketId { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Problema { get; set; } = string.Empty;
        public string Solucion { get; set; } = string.Empty;
        public string? Categoria { get; set; }
        public DateTime Fecha { get; set; }

    }
}
