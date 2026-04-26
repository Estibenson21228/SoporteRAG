using SoporteRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.DTOs
{
    public class SimilarityResult
    {
        public Embedding Embedding { get; set; } = null!;
        public double Score { get; set; }
        public string TipoRecurso { get; set; } = string.Empty;
        public int RecursoId { get; set; }
        public string TextoOriginal { get; set; } = string.Empty;
        public DocumentChunk? DocumentChunk { get; set; }

    }
}
