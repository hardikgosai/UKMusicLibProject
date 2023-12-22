using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UKMusicLibProject.Models
{
    public class UserViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; }

        public string ContactNo { get; set; }
    }
}
