using SoporteRAG.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace SoporteRAG.Infrastructure.Embeddings
{
    public class FakeEmbeddingService: IEmbeddingService
    {

        public Task<string> GenerateEmbeddingAsync(string text)
        {
            //Simulacion: Generemos 10 numeros aleatorios

            var random = new Random();
            var vector = Enumerable.Range(0, 10).Select(_ => random.NextDouble()).ToArray();

            var json = JsonSerializer.Serialize(vector);

            return Task.FromResult(json);
        }


    }
}
