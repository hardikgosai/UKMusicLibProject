namespace UKMusicLibProject.DTO
{
    public class ConcertDTO
    {
        public int ConcertId { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string TicketSold { get; set; }
        public string RevenueGenerated { get; set; }
    }
}
