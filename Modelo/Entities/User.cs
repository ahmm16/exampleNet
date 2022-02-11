using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public bool MustChangePassword { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateTime? LastAuthenticateDateTime { get; set; }
    }
}
