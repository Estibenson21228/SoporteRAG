using SoporteRAG.Application.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Services
{
    public class EvaluationService
    {
        private readonly ISemanticSearchService _searchService;

        public EvaluationService(ISemanticSearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<double> CalculatePrecisionAtk(int k=3)
        {
            int total = GoldenSet.Items.Count;
            int correct = 0;

            foreach (var item in GoldenSet.Items)
            {
                var results = await _searchService.SearchSimilarTicketAsync(item.Question, k);
                if (results.Any(r=>r.TipoRecurso.Contains(item.ExpectedSource)))
                {
                    correct++;
                }
            }

            return (double)correct / total;
        }

        public async Task<double> CalculateAnswerRelevance()
        {
            int total = GoldenSet.Items.Count;
            int correct = 0;

            foreach (var item in GoldenSet.Items)
            {
                var results = await _searchService.SearchSimilarTicketAsync(item.Question, 3);
                if (results.Any())
                {
                    var top = results.First();

                    if (top.Embedding.TextoOriginal.ToLower().Contains(item.ExpectedAnswerKeyword.ToLower()))
                    {
                        correct++;
                    }
                }
            }

            return (double)correct / total;

        }


        public async Task<object> RunExperiment(int chunkSize, int topK)
        {
            int total = GoldenSet.Items.Count;

            int hitsPrecision = 0;
            int hitsRecall = 0;
            double reciprocalRankSum = 0;
            long totalLatencyMs = 0;

            foreach (var item in GoldenSet.Items)
            {
                var sw = Stopwatch.StartNew();

                var results = await _searchService.SearchSimilarTicketAsync(item.Question, topK);

                sw.Stop();
                totalLatencyMs += sw.ElapsedMilliseconds;

                var expected = item.ExpectedSource.ToLower();

                var ranked = results.ToList();

                //Precision@K (¿aparece en topK?)
                bool foundInTopK = ranked.Any(r =>
                    r.TextoOriginal.ToLower().Contains(expected)
                );

                if (foundInTopK)
                    hitsPrecision++;

                //Recall@K (en este dataset simplificado = mismo que precision)
                if (foundInTopK)
                    hitsRecall++;

                //MRR
                var index = ranked.FindIndex(r =>
                    r.TextoOriginal.ToLower().Contains(expected)
                );

                if (index >= 0)
                {
                    reciprocalRankSum += 1.0 / (index + 1);
                }
            }

            return new
            {
                ChunkSize = chunkSize,
                TopK = topK,
                PrecisionAtK = (double)hitsPrecision / total,
                RecallAtK = (double)hitsRecall / total,
                MRR = reciprocalRankSum / total,
                AverageLatencyMs = totalLatencyMs / total
            };
        }

    }
}
