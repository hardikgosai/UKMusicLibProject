using System.ComponentModel.DataAnnotations;
namespace Domain.Models
{
    public class Concerts
    {
        [Key]
        public int ConcertId { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string TicketSold { get; set; }
        public string RevenueGenerated { get; set; }
    }
}
