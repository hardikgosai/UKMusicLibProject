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
    public class UserAPIController : ControllerBase
    {
        IUserRepository iuserRepository;
        public UserAPIController(IUserRepository _iuserRepository)
        {
            iuserRepository = _iuserRepository;           
        }
        [HttpGet("SingleUser")]
        public ActionResult GetSingleUsers(int id)
        {
            UserDTo userDto = new UserDTo();
            User userEntity = iuserRepository.GetUser(id);
           
            userDto.FirstName = userEntity.FirstName;
            userDto.LastName = userEntity.LastName;
            userDto.Address = userEntity.Address;
            userDto.Email = userEntity.Email;
            userDto.ContactNo = userEntity.ContactNo;
            userDto.Password = userEntity.Password;
            userDto.Name = $"{userEntity.FirstName} {userEntity.LastName}";
            userDto.UserName = userEntity.UserName;
            return Ok(userDto);

        }


        [HttpGet("ListUsers")]
        public ActionResult ListUsers()
        {
            List<UserDTo> lstUser = new List<UserDTo>();
            iuserRepository.GetUsers().ToList().ForEach(u =>
            {
                UserDTo userDto = null;
                
                userDto = new UserDTo()
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    Password = u.Password,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Name = $"{u.FirstName} {u.LastName}",
                    ContactNo = u.ContactNo,
                    Address = u.Address


                };
                lstUser.Add(userDto);
            });
            return Ok(lstUser);

        }

        [HttpPost("CreateUser")]
        public int CreateUser(UserDToCreate model)
        {
            User userEntity = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                Address = model.Address,
                ContactNo = model.ContactNo,
                LastName = model.LastName
                    
            };
            iuserRepository.InsertUser(userEntity);
            return 1;
        }

        [HttpPut("UpdateUser")]
        public int UpdateUser(UserDTo model)
        {
            User userEntity = new User
            {
                UserId = model.UserId,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                Address = model.Address,
                ContactNo = model.ContactNo,
                LastName = model.LastName,
                   
            };
            iuserRepository.UpdateUser(userEntity);
            return 1;
        }
        [HttpDelete("DeleteUser")]
        public int DeleteUser(long id)
        {
            iuserRepository.DeleteUser(id);
            return 1;
        }
    }
}
