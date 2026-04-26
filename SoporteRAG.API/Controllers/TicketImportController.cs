using Microsoft.AspNetCore.Mvc;
using SoporteRAG.Application.Interfaces;

namespace SoporteRAG.API.Controllers
{
    [ApiController]
    [Route("api/tickets/import")]
    public class TicketImportController : ControllerBase
    {
        private readonly ITicketCsvImportService _importService;
        public TicketImportController(ITicketCsvImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("csv")]
        public async Task<IActionResult> ImportCsv(IFormFile file)
        {
            try
            {
                var total = await _importService.ImportAsync(file);

                return Ok(new
                {
                    message = "CSV importado correctamente.",
                    ticketsImportados = total
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
    }
}
