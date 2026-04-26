using SoporteRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Interfaces
{
    public interface IDocumentChunkRepository
    {
        Task<List<DocumentChunk>> GetAllAsync();
    }
}
