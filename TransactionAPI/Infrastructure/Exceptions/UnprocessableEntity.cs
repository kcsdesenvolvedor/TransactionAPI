namespace TransactionAPI.Infrastructure.Exceptions
{
    public class UnprocessableEntity : Exception
    {
        public UnprocessableEntity(string message) : base(message)
        {
        }
    }
}
