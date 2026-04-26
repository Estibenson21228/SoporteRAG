using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Domain.Entities;
using SoporteRAG.Infrastructure.Data;

namespace SoporteRAG.Infrastructure.Repositories
{
    public class DocumentChunkRepository:IDocumentChunkRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentChunkRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<List<DocumentChunk>> GetAllAsync()
        {
            return await _context.DocumentChunks
                .Where(d => d.EmbeddingVector != null)
                .ToListAsync();
        }
    }
}
