namespace UKMusicLibProject.DTO
{
    public class UserDToCreate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ContactNo { get; set; }
    }
}
