using System.ComponentModel.DataAnnotations;

namespace TransactionAPI.Controllers.Dtos
{
    public class TransactionRequestDTO
    {
        [Required]
        public double TransactionValue { get; set; }
        [Required]
        public DateTimeOffset TrasactionDate { get; set; }
    }
}
