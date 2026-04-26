using CsvHelper.Configuration;
using SoporteRAG.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Infrastructure.Mappings
{
    public class TicketCsvMap: ClassMap<TicketCsvDto>
    {
        public TicketCsvMap()
        {
            Map(m => m.TicketId).Name("ticketId", "TicketId", "Ticket ID");
            Map(m => m.Titulo).Name("titulo", "Titulo", "Título");
            Map(m => m.Problema).Name("problema", "Problema");
            Map(m => m.Solucion).Name("solucion", "Solucion", "Solución");
            Map(m => m.Categoria).Name("categoria", "Categoria", "Categoría");
            Map(m => m.Fecha).Name("fecha", "Fecha");
        }
    }
}
