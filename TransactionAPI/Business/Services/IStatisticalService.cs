using TransactionAPI.Controllers.Dtos;

namespace TransactionAPI.Business.Services
{
    public interface IStatisticalService
    {
        StatisticalRequestDTO CalculateStatistical(int? parameterInSeconds);
    }
}
