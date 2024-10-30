using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiarioOnline.Data.Models;

namespace DiarioOnline.Data.Context
{
    public class DiarioContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = Environment.GetEnvironmentVariable("DiarioConnection");
            optionsBuilder.UseSqlServer(config);
        }
        private readonly IConfiguration? _configuration;

        public DiarioContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DiarioContext()
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<RegistroDiario> RegistroDiario { get; set; }
        public DbSet<Diario> Diario { get; set; }
    }
}
