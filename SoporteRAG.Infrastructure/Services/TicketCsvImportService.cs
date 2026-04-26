using Microsoft.AspNetCore.Http;
using SoporteRAG.Application.DTOs;
using SoporteRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using SoporteRAG.Application.Interfaces;
using SoporteRAG.Infrastructure.Mappings;

namespace SoporteRAG.Application.Services
{
    public class TicketCsvImportService:ITicketCsvImportService
    {
        private readonly ITicketService _ticketService;

        public TicketCsvImportService(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<int> ImportAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Debe seleccionar un archivo CSV.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".csv")
                throw new Exception("Solo se permiten archivos CSV.");

            using var stream = file.OpenReadStream();

            // Usa UTF8. Si tu archivo viene con caracteres raros, guárdalo como UTF-8 desde Excel.
            using var reader = new StreamReader(stream, Encoding.UTF8);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null,
                MissingFieldFound = null,
                HeaderValidated = null,
                PrepareHeaderForMatch = args => args.Header.Trim()
            };

            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<TicketCsvMap>();
            var records = csv.GetRecords<TicketCsvDto>().ToList();


            int imported = 0;

            foreach (var record in records)
            {
                var ticket = new Ticket
                {
                    TicketId = record.TicketId,
                    Titulo = record.Titulo,
                    Problema = record.Problema,
                    Solucion = record.Solucion,
                    Categoria = record.Categoria,
                    Fecha = record.Fecha
                };

                await _ticketService.CreateTicketAsync(ticket);
                imported++;
            }

            return imported;
        }


    }
}
