using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.DTOs
{
    public class RagResponseDto
    {
        public string Respuesta { get; set; } = string.Empty;

        public List<RagSourceDto> Fuentes { get; set; } = new();
    }
}
