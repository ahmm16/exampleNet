using Microsoft.EntityFrameworkCore;
using Modelo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.Context
{
    public class EjemploContext: DbContext
    {
        public EjemploContext(DbContextOptions<EjemploContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Rol> Roles { get; set; }

    }
}
