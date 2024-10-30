using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioOnline.DTO
{
    public class RegistroDiarioDTO
    {
        public Guid? Id { get; set; }
        public string? Registro { get; set; }
        public Guid DiarioId { get; set; }
        public DiarioDTO? Diario { get; set; }
        public byte[]? MidiaRegistroBytes { get; set; }
        public string? MidiaRegistroBase64
        {
            get
            {
                if (MidiaRegistroBytes != null)
                    return Convert.ToBase64String(MidiaRegistroBytes);
                else
                    return string.Empty;
            }
        }
        public IFormFile? MidiaRegistro { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { private get; set; }
    }
}
