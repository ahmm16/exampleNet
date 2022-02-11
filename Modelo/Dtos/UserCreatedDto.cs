using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo.Dtos
{
    public class UserCreatedDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
