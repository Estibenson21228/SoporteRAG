using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string TicketId { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Problema { get; set; } = string.Empty;
        public string Solucion { get; set; } = string.Empty;
        public string? Categoria { get;set; } 

        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        

    }
}
