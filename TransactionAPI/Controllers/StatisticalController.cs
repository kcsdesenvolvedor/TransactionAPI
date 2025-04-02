using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactionAPI.Business.Services;

namespace TransactionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticalController : Controller
    {
        private readonly IStatisticalService _statisticalService;

        public StatisticalController(IStatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }

        [HttpGet("/GetStatistics")]
        [SwaggerOperation(Summary = "Endpoint responsável por buscar estatísticas de transações")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Busca efetuada com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Erro na busca de estatística de transações")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Erro interno do servido")]
        public IActionResult GetStatistics([FromQuery] int? parameterInSeconds)
        {
            var statistical = _statisticalService.CalculateStatistical(parameterInSeconds);
            return Ok(statistical);
        }
    }
}
