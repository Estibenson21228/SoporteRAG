using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SoporteRAG.API.DTOs;
using SoporteRAG.Application.DTOs;
using SoporteRAG.Application.Interfaces;

namespace SoporteRAG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RagController : ControllerBase
    {
        private readonly IRagService _ragService;

        public RagController(IRagService ragService)
        {
            _ragService = ragService;
        }

        [HttpPost("preguntar")]
        public async Task<IActionResult> Search([FromBody] AskRequest request)
        {

            var result = await _ragService.AskAsync(request.Question);
            return Ok(result);
        }


        
    }
}
