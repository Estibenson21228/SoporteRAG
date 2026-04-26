using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Domain.Entities;
using DocumentFormat.OpenXml.Packaging;
using UglyToad.PdfPig;
using System.Text;
using SoporteRAG.Infrastructure.Data;


namespace SoporteRAG.Infrastructure.Services
{
    public class DocumentProcessingService:IDocumentProcessingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmbeddingService _embeddingService;

        private readonly string[] allowedExtensions = { ".pdf", ".txt", ".docx" };

        public DocumentProcessingService(ApplicationDbContext context, IEmbeddingService embeddingService)
        {
            _context = context;
            _embeddingService = embeddingService;
        }

        public async Task ProcessDocumentAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new Exception("Tipo de archivo no permitido. Solo PDF, TXT, DOCX");

            string fullText = await ExtractTextAsync(file, extension);

            var chunks = SplitIntoChunks(fullText, 600)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .ToList();

            foreach (var chunk in chunks)
            {
                var cleanChunk = chunk.Trim();

                var embeddingText = $"""
        Documento: {file.FileName}
        Contenido:
        {cleanChunk}
        """;

                var embeddingJson = await _embeddingService.GenerateEmbeddingAsync(embeddingText);

                var documentChunk = new DocumentChunk
                {
                    NombreDocumento = file.FileName,
                    Contenido = cleanChunk,
                    FechaIndexacion = DateTime.UtcNow
                };

                _context.DocumentChunks.Add(documentChunk);
                await _context.SaveChangesAsync();

                var embedding = new Embedding
                {
                    TipoRecurso = "Documento",
                    RecursoId = documentChunk.Id,
                    TextoOriginal = embeddingText,
                    EmbeddingVector = embeddingJson,
                    FechaCreacion = DateTime.UtcNow
                };

                _context.Embeddings.Add(embedding);
            }

            await _context.SaveChangesAsync();
        }


        private async Task<string> ExtractTextAsync(IFormFile file, string extension)
        {
            using var stream = file.OpenReadStream();

            if (extension == ".txt")
            {
                using var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }

            if (extension ==".pdf")
            {
                var sb = new StringBuilder();
                using var pdf = PdfDocument.Open(stream);
                foreach (var page in pdf.GetPages())
                {
                    sb.AppendLine(page.Text);
                }

                return sb.ToString();
            }

            if (extension==".docx")
            {
                using var doc = WordprocessingDocument.Open(stream, false);
                var body = doc.MainDocumentPart?.Document.Body;
                return body?.InnerText ?? string.Empty;
            }

            throw new Exception("Formato no soportado.");
        }

        private List<string> SplitIntoChunks(string text, int chunkSize)
        {
            var chunks = new List<string>();
            for (int i=0; i<text.Length; i+=chunkSize)
            {
                chunks.Add(text.Substring(i, Math.Min(chunkSize, text.Length-i)));
            }
            return chunks;
        }

    }
}
