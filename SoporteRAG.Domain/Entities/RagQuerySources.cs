using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Domain.Entities
{
    public class RagQuerySources
    {
        public int Id { get; set; }
        public int RagQueryId { get; set; }
        public string TipoRecurso { get; set; } = string.Empty;
        public string IdentificadorRecurso { get; set; } = string.Empty;
        public string FragmentoUsado { get; set; }=string.Empty;

        public RagQuery RagQuery { get; set; } = null!;
    }
}
