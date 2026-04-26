using Microsoft.EntityFrameworkCore;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Domain.Entities;
using SoporteRAG.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Infrastructure.Repositories
{
    public class EmbeddingRepository: IEmbeddingRepository
    {
        private readonly ApplicationDbContext _context;

        public EmbeddingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Embedding embedding)
        {
            await _context.Embeddings.AddAsync(embedding);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Embedding>> GetByTipoAsync(string tipoRecurso)
        {
            return await _context.Embeddings
                .Where(e => e.TipoRecurso == tipoRecurso)
                .ToListAsync();
        }

        public async Task<List<Embedding>> GetAllAsync()
        {
            return await _context.Embeddings
                .ToListAsync();
        }

    }
}
