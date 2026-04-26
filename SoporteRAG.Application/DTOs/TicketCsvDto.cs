using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.DTOs
{
    public class TicketCsvDto
    {
        public string TicketId { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Problema { get; set; } = string.Empty;
        public string Solucion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}
