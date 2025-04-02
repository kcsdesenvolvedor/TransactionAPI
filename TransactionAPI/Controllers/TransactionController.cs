using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactionAPI.Business.Services;
using TransactionAPI.Controllers.Dtos;

namespace TransactionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("/AddTransaction")]
        [SwaggerOperation(Summary = "Endpoint responsável por adicionar transações")]
        [SwaggerResponse(StatusCodes.Status201Created, Description = "Transação criada com sucesso")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Description = "Campos não atendem aos requisitos da transação")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Erro de requisição")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Erro interno do servido")]
        public IActionResult AddTransaction([FromBody] TransactionRequestDTO transactionRequest)
        {
            _transactionService.AddTransaction(transactionRequest);
            return Created();
        }

        [HttpDelete("/DeleteTransactions")]
        [SwaggerOperation(Summary = "Endpoint responsável por deletar transações")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Transação deletada com sucesso")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Erro de requisição")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Erro interno do servido")]
        public IActionResult DeleteTransactions()
        {
            _transactionService.DeleteTransactions();
            return Ok();
        }
    }
}
