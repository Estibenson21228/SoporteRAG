using SoporteRAG.Application.DTOs;
using SoporteRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Services
{
    public interface ISemanticSearchService
    {
        Task<List<SimilarityResult>> SearchSimilarTicketAsync(string question, int topK = 3);

    }
}
