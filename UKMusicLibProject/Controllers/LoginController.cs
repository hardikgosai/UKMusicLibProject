using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UKMusicLibProject.ViewModel;

namespace UKMusicLibProject.Controllers
{
    public class LoginController : Controller
    {
        HttpClient _client;
        IConfiguration configuration;
        public LoginController(IConfiguration _configuration)
        {
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel modelLogin)
        {
            if (ModelState.IsValid)
            {
                List<UserLoginViewModel> lstUsers = new List<UserLoginViewModel>();
                HttpResponseMessage res = await _client.GetAsync("api/LoginAPI/CheckListofUsers");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    lstUsers = JsonConvert.DeserializeObject<List<UserLoginViewModel>>(result);

                    var Data = lstUsers.FirstOrDefault(u => u.Email == modelLogin.Email);
                    if (Data != null)
                    {
                        bool isValid = (Data.Email == modelLogin.Email && Data.Password == modelLogin.Password);
                        if (isValid)
                        {
                            //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, modelLogin.Password) },
                            //    CookieAuthenticationDefaults.AuthenticationScheme);
                            //var principle = new ClaimsPrincipal(identity);
                            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);

                            // HttpContext.Session.SetString("UserName", modelLogin.Email);
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            TempData["errorPassword"] = "Invalid Password!";
                            return View(modelLogin);
                        }
                    }
                    else
                    {
                        TempData["errorUsername"] = "User not found!";
                        return View(modelLogin);
                    }
                }
                else
                {
                    TempData["errorAPi"] = "Can't connect to Api";
                    return View(modelLogin);
                }
            }
            else
            {
                return View(modelLogin);
            }
        }
    }
}
