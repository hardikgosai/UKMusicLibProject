using Microsoft.AspNetCore.Mvc;

namespace UKMusicLibProject.ViewModel
{
    public class UserLoginViewModel
    {
        [HiddenInput]
        public Int32 Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
