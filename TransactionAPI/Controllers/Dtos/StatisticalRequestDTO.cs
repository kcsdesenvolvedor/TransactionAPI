namespace TransactionAPI.Controllers.Dtos
{
    public class StatisticalRequestDTO
    {
        public StatisticalRequestDTO()
        {
            Sum = 0;
            Avg = 0;
            Max = 0;
            Min = 0;
            Count = 0;
        }
        public double Sum { get; set; }
        public double Avg { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public long Count { get; set; }
    }
}
