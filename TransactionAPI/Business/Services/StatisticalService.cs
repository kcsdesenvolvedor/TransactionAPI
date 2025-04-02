using TransactionAPI.Controllers.Dtos;

namespace TransactionAPI.Business.Services
{
    public class StatisticalService : IStatisticalService
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<StatisticalService> _log;

        public StatisticalService(ITransactionService transactionService, ILogger<StatisticalService> log)
        {
            _transactionService = transactionService;
            _log = log;
        }

        public StatisticalRequestDTO CalculateStatistical(int? parameterInSeconds)
        {
            _log.LogInformation($"Iniciando calculo de estatística de transações no intervalo: {parameterInSeconds}");
            var transactions = _transactionService.GetTransactions(parameterInSeconds);

            var transactionSummarized = transactions.Aggregate(new StatisticalRequestDTO(), (acc, t) =>
            {
                acc.Sum += t.TransactionValue;
                acc.Count++;
                acc.Max = acc.Max > t.TransactionValue ? acc.Max : t.TransactionValue;
                acc.Min = acc.Min < t.TransactionValue && acc.Min != 0 ? acc.Min : t.TransactionValue;

                return acc;
            });

            _log.LogInformation("Calculo de estatística finalizado");
            return new StatisticalRequestDTO
            {
                Sum = transactionSummarized.Sum,
                Avg = transactionSummarized.Count == 0 ? 0 : transactionSummarized.Sum / transactionSummarized.Count,
                Max = transactionSummarized.Max,
                Min = transactionSummarized.Min,
                Count = transactionSummarized.Count
            };
        }
    }
}
