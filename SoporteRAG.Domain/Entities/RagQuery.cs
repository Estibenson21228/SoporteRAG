using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Domain.Entities
{
    public class RagQuery
    {
        public int Id { get; set; }
        public string Pregunta { get; set; } = string.Empty;
        public string? RespuestaGenerada { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }

        public ICollection<RagQuerySources> Sources { get; set; } = new List<RagQuerySources>();
    }
}
