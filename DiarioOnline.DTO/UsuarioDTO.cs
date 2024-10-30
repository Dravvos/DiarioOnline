using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioOnline.DTO
{
    public class UsuarioDTO
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? UserName { get; set; }
        public string? Hash { get; set; }
        public string? Salt { get; set; }
        public byte[]? FotoPerfil{ get; set; }
        public string? FotoPerfilBase64 { get; set; }
        public IFormFile? FotoPerfilFormFile { get; set; }
        public DateTime DataInclusao { private get; set; }
        public DateTime? DataAlteracao { private get; set; }
    }
}
