using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UKMusicLibProject.DTO;
using UKMusicLibProject.Models;

namespace UKMusicLibProject.Controllers
{
    public class ConcertController : Controller
    {
        HttpClient _client;
        IConfiguration configuration;
        public ConcertController(IConfiguration _configuration)
        {
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public async Task<IActionResult> Index()
        {
            List<ConcertViewModel> model = new List<ConcertViewModel>();
            HttpResponseMessage res = await _client.GetAsync("api/ConcertAPI/ListConcerts");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ConcertViewModel>>(result);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddConcert()
        {
            ConcertViewModel model = new ConcertViewModel();
            model.Date = DateTime.Now.Date;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddConcert(ConcertViewModel model)
        {
            if (ModelState.IsValid)
            {
                ConcertDTO objConcertDTO = new ConcertDTO()
                {
                    ConcertId = model.ConcertId,
                    Date = model.Date,
                    Location = model.Location,
                    RevenueGenerated = model.RevenueGenerated,
                    TicketSold = model.TicketSold,

                };
                var response = await _client.PostAsJsonAsync("api/ConcertAPI/CreateConcert", objConcertDTO);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }

            }
            return View(model);
        }

    }
}
