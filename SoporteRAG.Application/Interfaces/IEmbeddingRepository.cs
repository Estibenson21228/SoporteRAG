using SoporteRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Interfaces
{
    public interface IEmbeddingRepository
    {
        Task AddAsync(Embedding embedding);

        Task<List<Embedding>> GetByTipoAsync(string tipoRecurso);
        Task<List<Embedding>> GetAllAsync();

    }
}
