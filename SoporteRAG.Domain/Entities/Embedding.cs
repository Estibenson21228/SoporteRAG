using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Domain.Entities
{
    public class Embedding
    {
        public int Id { get; set; }
        public string TipoRecurso { get; set; } = string.Empty;
        public int RecursoId { get; set; }
        public string EmbeddingVector { get; set; } = string.Empty;
        public string TextoOriginal { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }

    }
}
