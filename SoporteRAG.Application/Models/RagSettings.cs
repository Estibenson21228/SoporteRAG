using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Models
{
    public class RagSettings
    {
        public int TopK { get; set; } = 3;
        public double MinScore { get; set; } = 0.35;
        public int ChunkSize { get; set; } = 600;
    }
}
