using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Contracts
    {
        [Key]
        public int ContractId { get; set; }
        public string ContractType  { get; set; }
        public string ContractWith { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Revenue { get; set; }
        public string Status { get; set; }
    }
}
