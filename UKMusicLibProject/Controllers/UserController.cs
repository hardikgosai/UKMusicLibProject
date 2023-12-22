using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UKMusicLibProject.DTO;
using UKMusicLibProject.Models;

namespace UKMusicLibProject.Controllers
{
    public class UserController : Controller
    {
        HttpClient _client;
        IConfiguration configuration;
        public UserController(IConfiguration _configuration)
        {
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> model = new List<UserViewModel>();
            HttpResponseMessage res = await _client.GetAsync("api/UserAPI/ListUsers");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<UserViewModel>>(result);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string empSearch)
        {
            List<UserViewModel> model = new List<UserViewModel>();
            HttpResponseMessage res = await _client.GetAsync("api/UserAPI/ListUsers");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<UserViewModel>>(result).ToList();
            }
            ViewData["UserDetail"] = empSearch;

            if (!string.IsNullOrEmpty(empSearch))
            {
                model = model.Where(x => x.FirstName.Contains(empSearch)).ToList();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            UserViewModelCreate model = new UserViewModelCreate();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(UserViewModelCreate model)
        {
            if (ModelState.IsValid)
            {
                UserDToCreate objUserDToCreate = new UserDToCreate()
                {
                    Id = model.UserId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ContactNo = model.ContactNo,
                    Address = model.Address,
                    Password = model.Password,
                    UserName = model.UserName
                };
                var response = await _client.PostAsJsonAsync("api/UserAPI/CreateUser", objUserDToCreate);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("RegistrationSuccess");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int? id)
        {
            UserViewModel model = new UserViewModel();
            string url = "api/UserAPI/SingleUser?id=";
            using (var response = await _client.GetAsync(url + id))
            {
                var result = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserViewModel>(result);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            //HttpClient client = _api.Initial();
            string url = "api/UserAPI/UpdateUser";
            if (ModelState.IsValid)
            {
                var response = await _client.PutAsJsonAsync(url, model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            UserViewModel model = new UserViewModel();
            string url = "api/UserAPI/SingleUser?id=";
            using (var response = await _client.GetAsync(url + id))
            {
                var reult = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserViewModel>(reult);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            UserViewModel model = new UserViewModel();
            string url = "api/UserAPI/SingleUser?id=";
            using (var response = await _client.GetAsync(url + id))
            {
                var reult = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserViewModel>(reult);
            }

            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            string url = "api/UserAPI/DeleteUser?id=";
            await _client.DeleteAsync(url + id);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> RegistrationSuccess()
        {
            
            return View();
        }

    }
}
