using LogicaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.Dtos;
using Modelo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EjemploCharla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IUserServices _userServices;
        public AuthController(IAuthServices authServices, IUserServices userServices)
        {
            _authServices = authServices;
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Post(LoginRequestDto login)
        {
            var user = await _authServices.Login(login);
            if (user == null)
            {
                //TODO: Falta forbidden
                return BadRequest();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<User>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userServices.GetUserById(Guid.Parse(userId));
            return user;
        }
    }
}
