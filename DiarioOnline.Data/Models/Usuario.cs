using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioOnline.Data.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; } 
        public string? UserName { get; set; }
        public string? Hash { get; set; }
        public string? Salt { get; set; }
        public byte[]? FotoPerfil { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
