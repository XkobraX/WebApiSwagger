using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using UserApi.Data.Entities;

namespace CoreCodeCamp.Data
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration _config;

        public UserContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }
        public DbSet<User> Users { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("CodeCamp"));
        }

        protected override void OnModelCreating(ModelBuilder bldr)
        {

            bldr.Entity<User>()
              .HasData(new User
              {
                  Id = 1,
                  Nome = "Pedro Josué",
                  Login = "PedroADM",
                  CPF = "059.268.940-93",
                  DataCadastro = DateTime.Now,
                  Email = "pedromail@pedro.com",
                  acessKey = "94be650011cf412ca906fc335f615cdc",
                  senha = "Pedro123"
                 

              });

          

        }

    }
}
