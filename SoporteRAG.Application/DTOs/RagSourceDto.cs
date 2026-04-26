using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.DTOs
{
    public class RagSourceDto
    {
        public string TipoFuente { get; set; } = string.Empty;

        public string Identificador { get; set; } = string.Empty;

        public string Fragmento { get; set; } = string.Empty;

        public double Score { get; set; }
    }
}
