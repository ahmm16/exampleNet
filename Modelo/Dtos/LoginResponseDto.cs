using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Dtos
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public bool MustChangePassword { get; set; }

        public DateTime? LastAuthenticateDateTime { get; set; }

        public string Token { get; set; }
    }
}
