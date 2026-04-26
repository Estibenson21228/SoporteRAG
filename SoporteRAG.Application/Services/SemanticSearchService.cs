using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SoporteRAG.Application.DTOs;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Domain.Entities;

namespace SoporteRAG.Application.Services
{
    public class SemanticSearchService:ISemanticSearchService
    {
        private readonly IEmbeddingService _embeddingService;
        private readonly IEmbeddingRepository _embeddingRepository;
        private readonly IDocumentChunkRepository _documentChunkRepository;
        public SemanticSearchService(IEmbeddingService embeddingService, IEmbeddingRepository embeddingRepository, IDocumentChunkRepository documentChunkRepository)
        {
            _embeddingService = embeddingService;
            _embeddingRepository = embeddingRepository;
            _documentChunkRepository = documentChunkRepository;
        }


        public async Task<List<SimilarityResult>> SearchSimilarTicketAsync(string question, int topK = 3)
        {
            var results = new List<SimilarityResult>();

            var questionEmbeddingJson = await _embeddingService.GenerateEmbeddingAsync(question);

            if (string.IsNullOrWhiteSpace(questionEmbeddingJson))
                return results;

            float[]? questionVector;

            try
            {
                questionVector = JsonSerializer.Deserialize<float[]>(questionEmbeddingJson);
            }
            catch
            {
                return results;
            }

            if (questionVector == null)
                return results;

            var embeddings = await _embeddingRepository.GetAllAsync();

            foreach (var embedding in embeddings)
            {
                if (string.IsNullOrWhiteSpace(embedding.EmbeddingVector))
                    continue;

                float[]? vector;

                try
                {
                    vector = JsonSerializer.Deserialize<float[]>(embedding.EmbeddingVector);
                }
                catch
                {
                    continue;
                }

                if (vector == null || vector.Length != questionVector.Length)
                    continue;

                var score = CosineSimilarity(questionVector, vector);

                results.Add(new SimilarityResult
                {
                    Embedding = embedding,
                    Score = score,
                    TipoRecurso = embedding.TipoRecurso,
                    RecursoId = embedding.RecursoId,
                    TextoOriginal = embedding.TextoOriginal
                });
            }

            return results
                .Where(x => x.Score >= 0.30)
                .OrderByDescending(x => x.Score)
                .Take(topK)
                .ToList();
        }


        private double CosineSimilarity(float[] vector1, float[] vector2)
        {
            double dotProduct = 0.0;
            double norm1 = 0.0;
            double norm2 = 0.0;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                norm1 += Math.Pow(vector1[i], 2);
                norm2 += Math.Pow(vector2[i], 2);
            }

            return dotProduct / (Math.Sqrt(norm1) * Math.Sqrt(norm2));
        }
    }
}
