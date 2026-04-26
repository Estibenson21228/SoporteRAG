using OpenAI;
using OpenAI.Embeddings;
using Microsoft.Extensions.Configuration;
using SoporteRAG.Application.Interfaces;
using System.Text.Json;

namespace SoporteRAG.Infrastructure.Embeddings
{
    public class OpenAIEmbeddingService:IEmbeddingService
    {
        private readonly OpenAIClient _client;
        private readonly string _model;

        public OpenAIEmbeddingService(IConfiguration config)
        {
            var apiKey = config["OpenAI:ApiKey"];
            _model = config["OpenAI:EmbeddingModel"] ?? "text-embedding-3-small";
            _client = new OpenAIClient(apiKey);
        }

        public async Task<string> GenerateEmbeddingAsync(string text)
        {
            var embeddingClient = _client.GetEmbeddingClient(_model);

            var response = await embeddingClient.GenerateEmbeddingAsync(text);

            var vector = response.Value.ToFloats();

            return JsonSerializer.Serialize(vector);
            
        }

    }
}
