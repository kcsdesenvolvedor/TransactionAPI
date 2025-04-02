
using TransactionAPI.Controllers.Dtos;
using TransactionAPI.Infrastructure.Exceptions;

namespace TransactionAPI.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private List<TransactionRequestDTO> _transactions = new List<TransactionRequestDTO>();
        private ILogger<TransactionService> _log;

        private const int PARAMETER_IN_SECONDS_DEFAULT = 60;

        public TransactionService(ILogger<TransactionService> log)
        {
            _log = log;
        }

        public List<TransactionRequestDTO> GetTransactions(int? parameterInSecond)
        {
            _log.LogInformation($"Buscando transações no seguinte intervalo de data: {parameterInSecond} segundos atrás");
            var intervalDate = DateTimeOffset.Now.AddSeconds(-parameterInSecond ?? -PARAMETER_IN_SECONDS_DEFAULT);

            _log.LogInformation($"Estatísticas retornadas com sucesso");
            return _transactions.Where(t => t.TrasactionDate >= intervalDate).ToList();
        }


        public void AddTransaction(TransactionRequestDTO transactionRequest)
        {
            _log.LogInformation("Iniciando processo para adicionar transações");

            if (transactionRequest.TrasactionDate > DateTimeOffset.Now)
            {
                _log.LogError("Data e hora maiores que a data atual");
                throw new UnprocessableEntity("Data e hora maiores que a data atual");
            }

            if (transactionRequest.TransactionValue < 0)
            {
                _log.LogError("Valor menor que zero");
                throw new UnprocessableEntity("Valor menor que zero");
            }

            _transactions.Add(transactionRequest);
            _log.LogInformation("Transação adicionada com sucesso");
        }

        public void DeleteTransactions()
        {
            _log.LogInformation("Iniciando processo para deletar transações");
            _transactions.Clear();
            _log.LogInformation("Transações deletadas com sucesso");
        }
    }
}
