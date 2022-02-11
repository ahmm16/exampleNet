using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo.Entities
{
    public class Rol
    {
        public Guid RolId { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
    }
}
