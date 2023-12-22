
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Artist 
    {
        public Artist() { }
        [Key]
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
       // public string? BirthPlace { get; set; }
        public Gender? Gender { get; set; }
        public byte[] ArtistPhoto { get; set; }
        public int Age { get; set; }
        //public Nationality? Nationality { get; set; }
        //public Profession Profession { get; set; }
        public string SortDescription { get; set; }
        public string LongDescription { get; set; }
        //public string? Awards { get; set; }
       // public string? Debut { get; set; }
        public string CurrentLocation { get; set; }
        public Country? Country { get; set; }
        // public string? School { get; set; }
        // public string? College { get; set; }
        // public string? Hobbies { get; set; }
        // public Relationship? MaritalStatus { get; set; }
        //  public decimal? Salary { get; set; }
        //  public decimal? NetWorth { get; set; }
        //public string? Email { get; set; }
        //public string? PhoneNo { get; set; }
      //  public DateTime? DOB { get; set; }
        public bool ShowInFront { get; set; }
        public int? ShowInFrontOrder      {
            get; set;
        }
    }

   
    public enum Gender
    {
        Male,
        Female
    }

    public enum Country
    {
        Norway,
        Oslo,
        UK,
        US,
        Canada
    }

    public enum Profession
    {
        Musician,
        Dancer,
        Painter,
        Singer,
        Director,
        Actor
    }

    public enum Relationship
    {
        Single,
        Married,
        InRelation,
        Complecated,

    }

    public class MasterInfo
    {
        public int id { get; set; }
        public string value { get; set; }
    }
}
