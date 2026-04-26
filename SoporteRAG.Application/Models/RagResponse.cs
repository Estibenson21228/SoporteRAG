using SoporteRAG.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Models
{

    public class RagResponse
    {
        public string Respuesta { get; set; } = string.Empty;
        public string Confianza { get; set; } = string.Empty;
        public List<RagSourceDto> Fuentes { get; set; } = new();
    }
}




