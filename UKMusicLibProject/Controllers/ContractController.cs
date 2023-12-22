using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Repository;
using UKMusicLibProject.DTO;
using UKMusicLibProject.Models;


namespace UKMusicLibProject.Controllers
{
    public class ContractController : Controller
    {
        HttpClient _client;
        IConfiguration configuration;
        public readonly IContractRepository iContractRepository;
        public ContractController(IContractRepository _iContractRepository,
             IConfiguration _configuration)
        {
            iContractRepository = _iContractRepository;
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            List<ContractViewModel> model = new List<ContractViewModel>();
            HttpResponseMessage res = await _client.GetAsync("api/Contract/ContractList");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ContractViewModel>>(result);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddContract()
        {
            ContractViewModel model = new ContractViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContract(ContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync("api/Contract/CreateContract", model);
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
