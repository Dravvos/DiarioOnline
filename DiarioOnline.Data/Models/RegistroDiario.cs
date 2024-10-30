using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioOnline.Data.Models
{
    public class RegistroDiario
    {
        public Guid Id { get; set; }
        public string Registro { get; set; }
        public Guid DiarioId { get; set; }
        public byte[]? MidiaRegistro { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao {get; set; }
    }
}
