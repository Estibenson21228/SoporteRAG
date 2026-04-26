using Microsoft.AspNetCore.Mvc;
using SoporteRAG.Application.Interfaces;

namespace SoporteRAG.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentProcessingService _service;
        
        public DocumentController(IDocumentProcessingService service)
        {
            _service = service;
        }

        [HttpPost("cargar")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Debe seleccionar un archivo");

            await _service.ProcessDocumentAsync(file);

            return Ok("Documento procesado e indexado correctamente.");
        }

    }
}
