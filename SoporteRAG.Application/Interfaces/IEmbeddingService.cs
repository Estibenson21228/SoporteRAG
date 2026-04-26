using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Interfaces
{
    public interface IEmbeddingService
    {
        Task<string> GenerateEmbeddingAsync(string text);
    }
}
