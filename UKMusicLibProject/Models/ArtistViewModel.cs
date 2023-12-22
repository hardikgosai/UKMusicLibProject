using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UKMusicLibProject.Models
{
    public class ArtistViewModel
    {
        [HiddenInput]
        public int ArtistId { get; set; }
        [Display(Name = "Name")]
        public string ArtistName { get; set; }
        //[Display(Name = "Birth Place")]
        //public string? BirthPlace { get; set; }
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }
        //[Display(Name = "Photo Path")]
        //public string? PhotoPath { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }
        //[Display(Name = "Nationality")]
        //public Nationality? Nationality { get; set; }
        //[Display(Name = "Profession")]
        //public Profession Profession { get; set; }
        [Display(Name = "Short Description")]
        public string SortDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        //[Display(Name = "Award")]
        //public string? Awards { get; set; }
        //[Display(Name = "Debut")]
        //public string? Debut { get; set; }
        [Display(Name = "Location")]
        public string CurrentLocation { get; set; }
        [Display(Name = "Country")]
        public Country? Country { get; set; }
        //[Display(Name = "School")]
        //public string? School { get; set; }
        //[Display(Name = "College")]
        //public string? College { get; set; }
        //[Display(Name = "Hobbies")]
        //public string? Hobbies { get; set; }
        //[Display(Name = "Relationship")]
        //public Relationship? MaritalStatus { get; set; }
        //[Display(Name = "Salary")]
        //public decimal? Salary { get; set; }
        //[Display(Name = "Net Worth")]
        //public decimal? NetWorth { get; set; }
        //[Display(Name = "Email")]
        //public string? Email { get; set; }
        //[Display(Name = "Phone No")]
        //public string? PhoneNo { get; set; }
        //[Display(Name = "Date of Birth")]
        //public DateTime? DOB { get; set; }
        [Display(Name = "Artist Photo")]
        public IFormFile? ArtistPic { get; set; }
        [Display(Name = "Display in Slide")]
        public bool ShowInFront { get; set; }
        [Display(Name = "Slide Order")]
        public int? ShowInFrontOrder { get; set; }

        public byte[] ArtistPhoto { get; set; }
        public string UploadedFilePath{ get; set; }

        public string? ImageDataUrl { get; set; }

    }
}
