using TransactionAPI.Controllers.Dtos;

namespace TransactionAPI.Business.Services
{
    public interface ITransactionService
    {
        List<TransactionRequestDTO> GetTransactions(int? parameterInSecond = 60);
        void AddTransaction(TransactionRequestDTO transactionRequest);
        void DeleteTransactions();
    }
}
