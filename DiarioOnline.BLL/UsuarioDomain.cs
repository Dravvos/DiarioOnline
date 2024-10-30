using DiarioOnline.Common;
using DiarioOnline.Data;
using DiarioOnline.Data.Models;
using DiarioOnline.DTO;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;

namespace DiarioOnline.BLL
{
    public class UsuarioDomain
    {
        public UsuarioDTO ObterUsuario(string usuarioLogin)
        {
            var retornoDTO = new UsuarioDTO();
            using (var con = new BaseDal().GetContext())
            {
                var usuario = con.Usuario.AsNoTracking().Where(x => x.UserName == usuarioLogin).Select(x => new
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Nome = x.Nome,
                    FotoPerfil = x.FotoPerfil,
                    Hash = x.Hash
                }).FirstOrDefault();
                retornoDTO = Map<UsuarioDTO>.Convert(usuario);
            }

            return retornoDTO;
        }
        public bool ValidaUsuario(string usuario, string senha)
        {
            if (!string.IsNullOrEmpty(usuario))
            {
                var usr = ObterUsuario(usuario);
                if (usr != null)
                {
                    if (EncriptionHelper.VerifyPassword(senha, usr.Hash))
                        return true;
                }
            }
            return false;
        }
        public UsuarioDTO ObterUsuario(Guid usuarioId)
        {
            var retornoDTO = new UsuarioDTO();
            using (var con = new BaseDal().GetContext())
            {
                var usuario = con.Usuario.AsNoTracking().Where(x => x.Id == usuarioId).Select(x => new
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Nome = x.Nome,
                    FotoPerfil = x.FotoPerfil
                }).FirstOrDefault();
                retornoDTO = Map<UsuarioDTO>.Convert(usuario);
            }
            return retornoDTO;
        }
        public bool CriarUsuario(Usuario model)
        {
            ValidarModel(model);
            model.Hash = EncriptionHelper.Hash(model.Hash, out string salt);
            model.Salt = salt;
            model.DataInclusao = DateTime.Now;
            using (var con = new BaseDal().GetContext())
            {
                con.Usuario.Add(model);
                return con.SaveChanges() > 0;
            }
        }

        public bool AtualizarUsuario(Usuario model)
        {
            ValidarModel(model);
            model.DataAlteracao = DateTime.Now;
            using (var con = new BaseDal().GetContext())
            {
                con.Usuario.Update(model);
                return con.SaveChanges() > 0;
            }
        }

        public bool DeletarUsuario(Guid usuarioId)
        {
            using (var con = new BaseDal().GetContext())
            {
                var usuario = con.Usuario.AsNoTracking().FirstOrDefault(x => x.Id == usuarioId);
                con.Usuario.Remove(usuario);
                return con.SaveChanges() > 0;
            }
        }
        public void ValidarModel(Usuario model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new ArgumentNullException("O nome do usuário não pode estar vazio");
            }
            if (string.IsNullOrEmpty(model.Hash))
            {
                throw new ArgumentNullException("O login do usuário não pode estar vazio");
            }
        }
    }
}
