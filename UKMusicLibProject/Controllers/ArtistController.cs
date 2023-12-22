using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Services.Repository;
using System.IO;
using UKMusicLibProject.DTO;
using UKMusicLibProject.Models;

namespace UKMusicLibProject.Controllers
{
  
    public class ArtistController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        HttpClient _client;
        IConfiguration configuration;
        private readonly IArtistRepository _artistService;
        public ArtistController(IArtistRepository artistService, IConfiguration _configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            this._artistService = artistService;

            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _environment = environment;

        }
        public async Task<IActionResult> Index()
        {
            List<ArtistViewModel> model = new List<ArtistViewModel>();
            HttpResponseMessage res = await _client.GetAsync("api/ArtistAPI/ListArtists");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ArtistViewModel>>(result).ToList();
            }

            return View(model);
        }

        [HttpGet("ListArtist")]
        public async Task<ActionResult> ListArtist()
        {
            List<ArtistViewModel> model = new List<ArtistViewModel>();
            HttpResponseMessage res = await _client.GetAsync("api/ArtistAPI/ListArtists");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ArtistViewModel>>(result).ToList();
            }
            model.ForEach(x => GetImageFromByteArray(x));
            return View(model);

        }


        [HttpGet("ArtistDetail")]
        public async Task<ActionResult> ArtistDetail(int id)
        {
            ArtistViewModel model = new ArtistViewModel();
            HttpResponseMessage res = await _client.GetAsync("api/ArtistAPI/SingleArtist?id="+id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ArtistViewModel>(result);
            }
            GetImageFromByteArray(model);
            return View(model);

        }

        public void GetImageFromByteArray(ArtistViewModel model)
        {
            try
            {
                if (model.ArtistPhoto != null && model.ArtistPhoto.Length > 0)
                {
                    //string wwwPath = this._environment.WebRootPath;
                    ////string contentPath = this._environment.ContentRootPath;
                    //// Generate a unique file name
                    //var fileName = Guid.NewGuid().ToString() + ".png";

                    //// Define the path to save the file
                    //var filePath = Path.Combine(wwwPath, "TempImage", fileName);

                    // Convert image to byte array

                    //Convert byte arry to base64string
                    string imreBase64Data = Convert.ToBase64String(model.ArtistPhoto);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    //System.IO.File.WriteAllBytes(filePath, model.ArtistPhoto);
                    //model.UploadedFilePath= filePath;
                    model.ImageDataUrl= imgDataURL;
                }
            }
            catch { 
            
            }
        }

        [HttpGet]
        public ActionResult AddArtist()
        {
            GetMasterData();
            //model.PhotoPath = "Dummy Path";
            return View();

        }

        void GetMasterData()
        {
            ViewBag.Gender = new SelectList(GetGender(), "id", "value");
            ViewBag.Relationship = new SelectList(GetRelationship(), "id", "value");
            ViewBag.Profession = new SelectList(GetProfession(), "id", "value");
            ViewBag.Country = new SelectList(GetCountry(), "id", "value");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddArtist(ArtistViewModel model)
        {
            bool isSaved = false;
            GetMasterData();
            UploadImage(model);
            ModelState.Clear();
            if (TryValidateModel(model))
            {

                ArtistDTO artist = new ArtistDTO()
                {
                    ArtistId = model.ArtistId,
                    //Age = model.Age,
                    ArtistName = model.ArtistName,
                    //Awards = model.Awards,
                    //BirthPlace = model.BirthPlace,
                    //College = model.College,
                    CurrentLocation = model.CurrentLocation,
                    //Debut = model.Debut,
                    //DOB = model.DOB,
                    //Email = model.Email,
                    //Gender = model.Gender,
                    //Hobbies = model.Hobbies,
                    LongDescription = model.LongDescription,
                    //MaritalStatus = model.MaritalStatus,
                    //Nationality = model.Nationality,
                    //NetWorth = model.NetWorth,
                    //PhoneNo = model.PhoneNo,
                   // PhotoPath = model.PhotoPath,
                    //Profession = model.Profession,
                    //Salary = model.Salary,
                    //School = model.School,
                    ShowInFront = model.ShowInFront,
                    ShowInFrontOrder = model.ShowInFrontOrder,
                    SortDescription = model.SortDescription,
                    //ArtistPic = model.ArtistPic,
                    Country = model.Country,
                   
                    Age = model.Age,
                    ArtistPhoto =model.ArtistPhoto,
                    Gender = model.Gender
                };

                HttpResponseMessage response = await _client.PostAsJsonAsync("api/ArtistAPI/CreateArtist", artist);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListArtists");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error try after some time");
                    return View(model);

                }

            }
            return View(model);
        }


        void UploadImage(ArtistViewModel model)
        {
            IFormFile file = model.ArtistPic;
            if (file != null && file.Length > 0)
            {
                // Check if the file is an image
                if (IsImageFile(file))
                {

                    //string wwwPath = this._environment.WebRootPath;
                    ////string contentPath = this._environment.ContentRootPath;
                    //// Generate a unique file name
                    //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    //// Define the path to save the file
                    //var filePath = Path.Combine(wwwPath, "TempImage", fileName);

                    // Save the file
                    //using (var stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    file.CopyToAsync(stream);
                    //    model.UploadedFilePath = filePath;
                    //}

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        model.ArtistPhoto = ms.ToArray();
                        model.UploadedFilePath = "Valid path";
                    }
                }
                else
                {
                    model.ArtistPhoto = null;
                    //model.UploadedFilePath = string.Empty;
                }
            }
            else
            {
                //todo if already uploaded image file use that file to insert or update
                model.ArtistPhoto = null;
                //model.UploadedFilePath = string.Empty;
            }
        }

        private bool IsImageFile(IFormFile file)
        {
            // Check if the file has a valid image file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
        public static Byte[] ToByteArray(Stream stream)
        {
            Int32 length = stream.Length > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(stream.Length);
            Byte[] buffer = new Byte[length];
            stream.Read(buffer, 0, length);
            return buffer;
        }

        [HttpGet("LoadArtist")]
        public ActionResult UpdateArtist(int id)
        {
            ArtistViewModel model = new ArtistViewModel();
            var res = _client.GetAsync("api/ArtistAPI/SingleArtist/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ArtistViewModel>(result);
            }

            ViewBag.Gender = new SelectList(GetGender(), "id", "value");
            ViewBag.Relationship = new SelectList(GetRelationship(), "id", "value");
            ViewBag.Profession = new SelectList(GetProfession(), "id", "value");
            ViewBag.Country = new SelectList(GetCountry(), "id", "value");
            return View(model);

        }

        [HttpPost("UpdateArtist")]
        public async Task<ActionResult> UpdateArtist(ArtistViewModel model)
        {
            bool isSaved = false;
            UploadImage(model);
            if (!ModelState.IsValid)
            {
                return View("LoadArtist", model.ArtistId);
            }
            else
            {
                ArtistDTO artist = new ArtistDTO()
                {
                    ArtistId = model.ArtistId,
                   // Age = model.Age,
                    ArtistName = model.ArtistName,
                    //Awards = model.Awards,
                   // BirthPlace = model.BirthPlace,
                   // College = model.College,
                    CurrentLocation = model.CurrentLocation,
                    //Debut = model.Debut,
                    //DOB = model.DOB,
                    //Email = model.Email,
                    //Gender = model.Gender,
                    //Hobbies = model.Hobbies,
                    LongDescription = model.LongDescription,
                    //MaritalStatus = model.MaritalStatus,
                    //Nationality = model.Nationality,
                    //NetWorth = model.NetWorth,
                   // PhoneNo = model.PhoneNo,
                   /// PhotoPath = model.PhotoPath,
                    //Profession = model.Profession,
                    //Salary = model.Salary,
                    //School = model.School,
                    ShowInFront = model.ShowInFront,
                    ShowInFrontOrder = model.ShowInFrontOrder,
                    SortDescription = model.SortDescription,
                    //ArtistPic = model.ArtistPic,
                    Age=model.Age,
                    Country=model.Country,  
                    Gender=model.Gender,    
                    ArtistPhoto=model.ArtistPhoto
                };
                JsonContent content = JsonContent.Create(artist);
                HttpResponseMessage response = await _client.PostAsync("api/ArtistAPI/UpdateArtist", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("LoadArtists");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error try after some time");
                    return View("LoadArtist", model.ArtistId);

                }

            }
            //if(isSaved)
            //    return RedirectToAction("ListArtists");
            //else
            //    return View("AddArtist", model);
        }

        List<MasterInfo> GetGender()
        {
            return Enum.GetValues(typeof(Gender))
                          .Cast<Gender>()
                          .Select(d => new MasterInfo
                          {
                              id = (int)d,
                              value = d.ToString()
                          })
                          .ToList();
        }

        List<MasterInfo> GetRelationship()
        {
            return Enum.GetValues(typeof(Relationship))
                          .Cast<Relationship>()
                          .Select(d => new MasterInfo
                          {
                              id = (int)d,
                              value = d.ToString()
                          })
                          .ToList();
        }

        List<MasterInfo> GetProfession()
        {
            return Enum.GetValues(typeof(Profession))
                          .Cast<Profession>()
                          .Select(d => new MasterInfo
                          {
                              id = (int)d,
                              value = d.ToString()
                          })
                          .ToList();
        }


        List<MasterInfo> GetCountry()
        {
            return Enum.GetValues(typeof(Country))
                          .Cast<Country>()
                          .Select(d => new MasterInfo
                          {
                              id = (int)d,
                              value = d.ToString()
                          })
                          .ToList();
        }
    }
}
