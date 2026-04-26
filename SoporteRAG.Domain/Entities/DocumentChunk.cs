using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Domain.Entities
{
    public class DocumentChunk
    {
        public int Id { get; set; }
        public string NombreDocumento { get; set; } = string.Empty;
        public int? NumeroPagina { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaIndexacion { get; set; }
        public string EmbeddingVector { get; set; } = string.Empty;

    }
}
