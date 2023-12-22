using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using UKMusicLibProject.DTO;

namespace UKMusicLibProject.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistAPIController : ControllerBase
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        IArtistRepository _iArtistRepository;
        

        public ArtistAPIController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IArtistRepository iArtistRepository)
        {
            _environment = environment;
            _iArtistRepository = iArtistRepository;
            
        }


        [HttpGet("SingleArtist")]
        public ActionResult GetSingleUsers(int id)
        {
            ArtistDTO artist = new ArtistDTO();
            var u = _iArtistRepository.GetArtists().FirstOrDefault(x=>x.ArtistId==id);

            artist = new ArtistDTO()
            {
                ArtistId = u.ArtistId,
             //   Age = u.Age,
                ArtistName = u.ArtistName,
             //   Awards = u.Awards,
             //   BirthPlace = u.BirthPlace,
              //  College = u.College,
                CurrentLocation = u.CurrentLocation,
            //    Debut = u.Debut,
             //   DOB = u.DOB,
                //Email = u.Email,
                //Gender = u.Gender,
            //    Hobbies = u.Hobbies,
                LongDescription = u.LongDescription,
            //    MaritalStatus = u.MaritalStatus,
            //    Nationality = u.Nationality,
             //   NetWorth = u.NetWorth,
                //PhoneNo = u.PhoneNo,
                //PhotoPath = u.PhotoPath,
            //    Profession = u.Profession,
            //    Salary = u.Salary,
             //   School = u.School,
                ShowInFront = u.ShowInFront,
                ShowInFrontOrder = u.ShowInFrontOrder,
                SortDescription = u.SortDescription,
                Country = u.Country,
                Age = u.Age,
                ArtistPhoto = u.ArtistPhoto,
                Gender = u.Gender
            };

            return Ok(artist);
        }


        [HttpGet("ListArtists")]
        public ActionResult ListArtists()
        {
            List<ArtistDTO> lstArtist = new List<ArtistDTO>();
            _iArtistRepository.GetArtists().ToList().ForEach(u =>
            {
                ArtistDTO artistDTO = null;
                artistDTO = new ArtistDTO()
                {
                    ArtistId = u.ArtistId,
                //    Age = u.Age,
                    ArtistName = u.ArtistName,
                //    Awards = u.Awards,
               //     BirthPlace = u.BirthPlace,
              //      College = u.College,
                    CurrentLocation = u.CurrentLocation,
                //    Debut = u.Debut,
                //    DOB = u.DOB,
                    //Email = u.Email,
                    //Gender = u.Gender,
               //     Hobbies = u.Hobbies,
                    LongDescription = u.LongDescription,
               //     MaritalStatus = u.MaritalStatus,
                //    Nationality = u.Nationality,
               //     NetWorth = u.NetWorth,
                   // PhoneNo = u.PhoneNo,
                   // PhotoPath = u.PhotoPath,
             //       Profession = u.Profession,
             //       Salary = u.Salary,
             //       School = u.School,
                    ShowInFront = u.ShowInFront,
                    ShowInFrontOrder = u.ShowInFrontOrder,
                    SortDescription = u.SortDescription,
                    Country = u.Country,
                    
                    Age = u.Age,
                    ArtistPhoto = u.ArtistPhoto,
                    Gender = u.Gender
                };
                lstArtist.Add(artistDTO);
            });
            return Ok(lstArtist);

        }


        [HttpPost("CreateArtist")]
        public int CreateArtist(ArtistDTO model)
        {
            Artist _artistEntity = new Artist
            {

                //ArtistId = model.ArtistId,
                //Age = model.Age,
                ArtistName = model.ArtistName,
                ///Awards = model.Awards,
               // BirthPlace = model.BirthPlace,
               // College = model.College,
                CurrentLocation = model.CurrentLocation,
                //Debut = model.Debut,
              //  DOB = model.DOB,
                //Email = model.Email,
                //Gender = model.Gender,
             //   Hobbies = model.Hobbies,
                LongDescription = model.LongDescription,
              //  MaritalStatus = model.MaritalStatus,
              //  Nationality = model.Nationality,
              //  NetWorth = model.NetWorth,
                //PhoneNo = model.PhoneNo,
               // PhotoPath = "dUMMY pATH",//UploadImage(model.ArtistPic),
              //  Profession = model.Profession,
               // Salary = model.Salary,
              //  School = model.School,
                ShowInFront = model.ShowInFront,
                ShowInFrontOrder = model.ShowInFrontOrder,
                SortDescription = model.SortDescription,
                Country= model.Country,
                ArtistId= model.ArtistId,
                Age =model .Age,
                ArtistPhoto= model.ArtistPhoto,
                Gender= model.Gender

            };



            if (_artistEntity.ArtistPhoto.Length==0)
                return 0;
            else
            {
                _iArtistRepository.InsertArtist(_artistEntity);
                return 1;
            }
        }

        string UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Check if the file is an image
                if (IsImageFile(file))
                {

                    string wwwPath = this._environment.WebRootPath;
                    string contentPath = this._environment.ContentRootPath;

                    string path = Path.Combine(this._environment.WebRootPath, "Images");
                    // Generate a unique file name
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Define the path to save the file
                    var filePath = Path.Combine(path, "Uploads", fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }

                    return filePath;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private bool IsImageFile(IFormFile file)
        {
            // Check if the file has a valid image file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }

        [HttpPut("UpdateArtist")]
        public int UpdateArtist(ArtistDTO model)
        {
            Artist _artistEntity = new Artist
            {
                ArtistId = model.ArtistId,
              //  Age = model.Age,
                ArtistName = model.ArtistName,
              //  Awards = model.Awards,
             //   BirthPlace = model.BirthPlace,
             //   College = model.College,
                CurrentLocation = model.CurrentLocation,
             //   Debut = model.Debut,
             //   DOB = model.DOB,
                //Email = model.Email,
                //Gender = model.Gender,
             //   Hobbies = model.Hobbies,
                LongDescription = model.LongDescription,
             //   MaritalStatus = model.MaritalStatus,
              //  Nationality = model.Nationality,
              //  NetWorth = model.NetWorth,
                //PhoneNo = model.PhoneNo,
               // PhotoPath = model.PhotoPath,
              //  Profession = model.Profession,
              //  Salary = model.Salary,
              //  School = model.School,
                ShowInFront = model.ShowInFront,
                ShowInFrontOrder = model.ShowInFrontOrder,
                SortDescription = model.SortDescription,
                Country = model.Country,
                
                Age = model.Age,
                ArtistPhoto = model.ArtistPhoto,
                Gender = model.Gender

            };
            _iArtistRepository.UpdateArtist(_artistEntity);
            return 1;

        }
        [HttpDelete("DeleteArtist")]
        public int DeleteArtist(long id)
        {
            _iArtistRepository.DeleteArtist(id);
            return 1;
        }
    }
}
