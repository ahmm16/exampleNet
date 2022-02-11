using Modelo.Dtos;
using Modelo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public interface IAuthServices
    {
        Task<LoginResponseDto> Login(LoginRequestDto login);

    }
}
