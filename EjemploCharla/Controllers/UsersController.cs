
using AccesoDatos.Context;
using LogicaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.Dtos;
using Modelo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploCharla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userServices.GetUsers();
        }

        [HttpGet("ById/{userId}")]
        [AllowAnonymous]
        public async Task<User> GetById(Guid userId)
        {
            return await _userServices.GetUserById(userId);
        }

        [HttpPost]
        public async Task<User> Post(UserCreatedDto user)
        {
            return await _userServices.AddUser(user);
        }

        [HttpPut]
        public async Task<User> Put(User user)
        {
            return await _userServices.UpdateUser(user);
        }

        [HttpDelete]
        public async Task<bool> Delete(Guid userId)
        {
            return await _userServices.DeleteUser(userId);
        }
    }
}
