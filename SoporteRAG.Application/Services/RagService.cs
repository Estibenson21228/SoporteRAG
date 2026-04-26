using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using SoporteRAG.Application.DTOs;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Application.Models;
using SoporteRAG.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;



namespace SoporteRAG.Infrastructure.Services
{
    public class RagService:IRagService
    {
        private readonly ISemanticSearchService _searchService;
        private readonly OpenAIClient _client;
        private readonly string _chatModel;
        private readonly RagSettings _ragSettings;

        public RagService(
            ISemanticSearchService searchService,
            IConfiguration config,
            IOptions<RagSettings> ragOptions)
        {
            _searchService = searchService;
            _client = new OpenAIClient(config["OpenAI:ApiKey"]);
            _chatModel = config["OpenAI:ChatModel"] ?? "gpt-4o-mini";
            _ragSettings = ragOptions.Value;
        }
        /*
        public async Task<RagResponseDto> AskAsync(string question)
        {
            var similarEmbeddings = await _searchService.SearchSimilarTicketAsync(question);

            var bestScore = similarEmbeddings.Max(x => x.Score);

            if (bestScore < 0.50)
            {
                return new RagResponseDto
                {
                    Respuesta = "No se encontró información en la base de conocimiento."
                };
            }

            var contextBuilder = new StringBuilder();

            foreach (var item in similarEmbeddings)
            {
                contextBuilder.AppendLine(item.Embedding.TextoOriginal);
                contextBuilder.AppendLine("-----");
            }

            var chatClient = _client.GetChatClient(_chatModel);

            var messages = new List<ChatMessage>
            {
                ChatMessage.CreateSystemMessage("""
                    Eres un asistente interno de soporte técnico corporativo.

                    Tu tarea es:
                    - Analizar la pregunta del usuario.
                    - Revisar el contexto proporcionado.
                    - Responde únicamente con información presente en el contexto.
                    - No agregues recomendaciones adicionales.
                    - No hagas suposiciones.
                    - No agregues información externa.
                    - No es necesario que el texto sea idéntico.
                    - Si el problema es similar, utiliza la solución encontrada:
                        "además quiero que respondas puedes de la siguintes manera o de forma similar:
                        prueba lo siguientes o algunos usuarios intentaron lo siguiente"
            

            

                    Responde de forma directa y concreta.
                    """
                ),

                ChatMessage.CreateUserMessage($"""
                CONTEXTO:
                {contextBuilder}

                PREGUNTA:
                {question}
                """)
            };

            var response = await chatClient.CompleteChatAsync(messages);

            var ragResponse = new RagResponseDto
            {
                Respuesta = response.Value.Content[0].Text
            };

            foreach (var item in similarEmbeddings)
            {
                ragResponse.Fuentes.Add(new RagSourceDto
                {
                    TipoFuente = item.Embedding.TipoRecurso,
                    Identificador = $"{item.Embedding.TipoRecurso}-{item.Embedding.Id}",
                    Fragmento = item.Embedding.TextoOriginal,
                    Score = item.Score
                });
            }

            return ragResponse;
        }*/



        public async Task<RagResponse> AskAsync(string question)
        {
            var results = await _searchService.SearchSimilarTicketAsync(question, _ragSettings.TopK);

            if (!results.Any())
            {
                return new RagResponse
                {
                    Respuesta = "No se encontró información en la base de conocimiento.",
                    Confianza = "Baja"
                };
            }

            var contextBuilder = new StringBuilder();

            foreach (var result in results)
            {
                contextBuilder.AppendLine($"Fuente: {result.TipoRecurso}-{result.RecursoId}");
                contextBuilder.AppendLine(result.TextoOriginal);
                contextBuilder.AppendLine("-----");
            }

            var fuentes = results.Select(r => new RagSourceDto
            {
                TipoFuente = r.TipoRecurso,
                Identificador = $"{r.TipoRecurso}-{r.RecursoId}",
                Fragmento = r.TextoOriginal,
                Score = r.Score
            }).ToList();

            
            var bestScore = results.Max(r => r.Score);
            var confianza = CalcularConfianza(bestScore);

            if (bestScore < _ragSettings.MinScore)
            {
                return new RagResponse
                {
                    Respuesta = "No se encontró información en la base de conocimiento.",
                    Confianza = confianza,
                    Fuentes = fuentes
                };
            }

            
            var chatClient = _client.GetChatClient(_chatModel);

            var messages = new List<ChatMessage>
    {
        ChatMessage.CreateSystemMessage(
            """
            Eres un asistente interno de soporte técnico corporativo.

            Reglas:
            - Responde únicamente usando el contexto proporcionado.
            - Si existe al menos un caso similar en el contexto, usa su solución.
            - No necesitas que el texto sea idéntico; si el problema coincide o es claramente similar, responde usando la solución encontrada.
            - No uses conocimiento externo.
            - No inventes pasos adicionales.
            - Solo responde "No se encontró información en la base de conocimiento." si NO hay ningún fragmento relacionado.

            Estilo:
            - Responde de forma breve y accionable.
            - Puedes iniciar con: "En un caso similar..."
            """
        ),
        ChatMessage.CreateUserMessage(
            $"""
            CONTEXTO:
            {contextBuilder}

            PREGUNTA DEL USUARIO:
            {question}
            """
        )
    };

            var response = await chatClient.CompleteChatAsync(messages);

            return new RagResponse
            {
                Respuesta = response.Value.Content[0].Text,
                Confianza=confianza,
                Fuentes = fuentes
            };
        }


        private string CalcularConfianza(double score)
        {
            if (score >= 0.70)
                return "Alta";

            if (score >= 0.45)
                return "Media";

            return "Baja";
        }


    }
}
