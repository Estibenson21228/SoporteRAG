using Microsoft.AspNetCore.Mvc;
using SoporteRAG.Application.Services;

namespace SoporteRAG.API.Controllers
{
    [ApiController]
    [Route("api/evaluation")]
    public class EvaluationController : ControllerBase
    {
        private readonly EvaluationService _evaluationService;
        
        public EvaluationController(EvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }


        [HttpGet]
        public async Task<IActionResult> Evaluate()
        {
            var precision = await _evaluationService.CalculatePrecisionAtk(3);
            var relavance = await _evaluationService.CalculateAnswerRelevance();

            return Ok(new
            {
                PrecisionAt3= precision,
                Relevance= relavance
            });
        }

        [HttpGet("experiments")]
        public async Task<IActionResult> RunExperiments()
        {
            var results = new List<object>();

            results.Add(await _evaluationService.RunExperiment(300, 3));
            results.Add(await _evaluationService.RunExperiment(300, 7));
            results.Add(await _evaluationService.RunExperiment(600, 3));
            results.Add(await _evaluationService.RunExperiment(600, 7));

            return Ok(results);
        }


    }
}
