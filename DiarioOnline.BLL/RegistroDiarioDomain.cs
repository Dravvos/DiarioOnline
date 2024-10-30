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
    public class RegistroDiarioDomain
    {
        public RegistroDiarioDTO ObterRegistroDiario(Guid registroDiarioId)
        {
            using (var con = new BaseDal().GetContext())
            {
                var registro = con.RegistroDiario.FirstOrDefault(x => x.Id == registroDiarioId);
                var dto = new RegistroDiarioDTO
                {
                    Id = registroDiarioId,
                    DiarioId = registro.DiarioId,
                    MidiaRegistroBytes = registro.MidiaRegistro,
                    Registro = registro.Registro,
                    DataInclusao = registro.DataInclusao,
                };
                return dto;
            }
        }
        public List<RegistroDiarioDTO> ObterRegistrosDiario(Guid diarioId)
        {
            using (var con = new BaseDal().GetContext())
            {
                var registros = con.RegistroDiario.Where(x => x.DiarioId == diarioId).ToList();
                var dtos = new List<RegistroDiarioDTO>();
                foreach (var registro in registros)
                {
                    dtos.Add(new RegistroDiarioDTO
                    {
                        Id = registro.Id,
                        DiarioId = registro.DiarioId,
                        MidiaRegistroBytes = registro.MidiaRegistro,
                        Registro = registro.Registro,
                        DataInclusao = registro.DataInclusao,
                    });
                }
                return dtos;
            }
        }
        public bool CriarRegistroDiario(RegistroDiario model)
        {
            ValidarModel(model);
            model.DataInclusao = DateTime.Now;
            using (var con = new BaseDal().GetContext())
            {
                con.RegistroDiario.Add(model);
                return con.SaveChanges() > 0;
            }
        }
        public bool AtualizarRegistroDiario(RegistroDiario model)
        {
            ValidarModel(model);
            model.DataAlteracao = DateTime.Now;
            using (var con = new BaseDal().GetContext())
            {
                con.RegistroDiario.Update(model);
                return con.SaveChanges() > 0;
            }
        }
        public bool DeletarRegistroDiario(Guid registroDiarioId)
        {
            using (var con = new BaseDal().GetContext())
            {
                var registro = con.RegistroDiario.First(x => x.Id == registroDiarioId);
                con.RegistroDiario.Remove(registro);
                return con.SaveChanges() > 0;

            }
        }
        public void ValidarModel(RegistroDiario model)
        {
            if (model.DiarioId == Guid.Empty)
                throw new ArgumentNullException("Preencha o Id do diário ao criar o registro");

            if (model.Registro.Length > 280)
                throw new ArgumentException("Só é permitido um registro de no máximo 280 caracteres");
        }
    }
}
