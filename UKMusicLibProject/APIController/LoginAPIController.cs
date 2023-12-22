using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using UKMusicLibProject.DTO;

namespace UKMusicLibProject.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        ILoginRepository loginRepository;
        public LoginAPIController(ILoginRepository _loginRepository)
        {
            loginRepository = _loginRepository;
        }

        [HttpGet("CheckListofUsers")]
        public ActionResult CheckListofUsers()
        {
            List<UserLoginDTO> lstUser = new List<UserLoginDTO>();
            loginRepository.GetUsers().ToList().ForEach(u =>
            {
                UserLoginDTO userLoginDTO = null;
                userLoginDTO = new UserLoginDTO()
                {
                    Id = u.UserId,
                    Email = u.Email,
                    Password = u.Password
                };
                lstUser.Add(userLoginDTO);
            });
            return Ok(lstUser);

        }
    }
}
