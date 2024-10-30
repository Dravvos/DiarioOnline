using DiarioOnline.Common;
using DiarioOnline.Data;
using DiarioOnline.Data.Models;
using DiarioOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioOnline.BLL
{
    public class DiarioDomain
    {
        public DiarioDTO ObterDiario(Guid diarioId)
        {
            using (var con = new BaseDal().GetContext())
            {
                var diario = con.Diario.FirstOrDefault(x => x.Id == diarioId);
                var dto = Map<DiarioDTO>.Convert(diario);
                return dto;
            }
        }
        public DiarioDTO ObterDiarioPorUsuario(Guid usuarioId)
        {
            using (var con = new BaseDal().GetContext())
            {
                var diario = con.Diario.FirstOrDefault(x => x.UsuarioId == usuarioId);
                var dto = Map<DiarioDTO>.Convert(diario);
                return dto;
            }
        }
        public bool CriarDiario(Diario model)
        {
            ValidarModel(model);
            model.DataInclusao = DateTime.Now;
            using (var con = new BaseDal().GetContext())
            {
                con.Diario.Add(model);
                return con.SaveChanges() > 0;
            }
        }
        public bool AtualizarDiario(Diario model)
        {
            ValidarModel(model);
            model.DataAlteracao = DateTime.Now;
            using (var con = new BaseDal().GetContext())
            {
                con.Diario.Add(model);
                return con.SaveChanges() > 0;
            }
        }
        public bool DeletarDiario(Guid diarioId)
        {
            using (var con = new BaseDal().GetContext())
            {
                var registrosDiario = con.RegistroDiario.Where(x => x.DiarioId == diarioId).ToList();
                con.RegistroDiario.RemoveRange(registrosDiario);
                if (con.SaveChanges() > 0 == false)
                    throw new Exception("Não foi possível deletar os registro do diário");

                var diario = con.Diario.First(x => x.Id == diarioId);
                con.Diario.Remove(diario);
                return con.SaveChanges() > 0;

            }
        }
        public void ValidarModel(Diario model)
        {
            if (model.UsuarioId == Guid.Empty)
                throw new ArgumentNullException("Preencha o Id do usuário ao criar o diário");
        }
    }
}
