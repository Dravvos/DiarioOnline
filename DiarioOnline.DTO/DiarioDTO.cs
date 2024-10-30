using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioOnline.DTO
{
    public class DiarioDTO
    {
        public Guid? Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataInclusao { private get; set; }
        public DateTime? DataAlteracao { private get; set; }
    }
}
