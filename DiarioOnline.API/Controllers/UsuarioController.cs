using DiarioOnline.BLL;
using DiarioOnline.Common;
using DiarioOnline.Data.Models;
using DiarioOnline.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace DiarioOnline.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsuarioController : Controller
    {
        [HttpGet("{login}"), Authorize]
        public IActionResult GetUsuario(string login)
        {
            var domain = new UsuarioDomain();
            UsuarioDTO retorno;
            try
            {
                retorno =  domain.ObterUsuario(login);
                if (retorno == null)
                    return Json(NotFound("Usuário não encontrado"));

                string msg = Validation(retorno.Id.GetValueOrDefault());
                if (string.IsNullOrEmpty(msg) == false)
                    return Unauthorized(msg);


            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            return Json(Ok(retorno));
        }

        [HttpGet("{usuarioId:Guid}"), Authorize]
        public IActionResult GetUsuario(Guid usuarioId)
        {
            string msg = Validation(usuarioId);
            if (string.IsNullOrEmpty(msg)==false)
                return Unauthorized(msg);

            var domain = new UsuarioDomain();
            UsuarioDTO retorno;
            try
            {
                retorno = domain.ObterUsuario(usuarioId);
                if (retorno == null)
                    return Json(NotFound("Usuário não encontrado"));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            return Json(Ok(retorno));
        }

        private string Validation(Guid usuarioId)
        {
            var authHeader = this.Request.Headers["Authorization"].ToString();
            string token = authHeader.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var claims = jsonToken.Claims;

            var userIdClaim = claims.FirstOrDefault(x => x.Type == "UsuarioId");
            if (usuarioId != Guid.Parse(userIdClaim.Value))
                return "Não é possível verificar nem alterar informações de outros usuários";
            return "";
        }

        [HttpPut, Authorize]
        public IActionResult UpdateUsuario([FromBody] UsuarioDTO usuarioUpdate)
        {
            string msg = Validation(usuarioUpdate.Id.GetValueOrDefault());
            if (string.IsNullOrEmpty(msg) == false)
                return Unauthorized(msg);

            if (usuarioUpdate == null)
                return Json(BadRequest(ModelState));

            if (usuarioUpdate.Id == null || usuarioUpdate.Id == Guid.Empty)
                return Json(BadRequest("Preencha o Id do usuário"));

            bool retorno;
            var domain = new UsuarioDomain();
            var usuario = Map<Usuario>.Convert(usuarioUpdate);
            try
            {
                if ( domain.ObterUsuario(usuarioUpdate.Id.Value) == null)
                    return Json(NotFound("Usuário não encontrado"));

                retorno =  domain.AtualizarUsuario(usuario);
            }
            catch (ArgumentNullException ex)
            {
                return Json(BadRequest(ex.Message));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            if (retorno == false)
                return Json(StatusCode(500, "Não foi possível atualizar o usuário"));
            else
                return Json(Ok("Usuário atualizado com sucesso"));
        }

        [HttpDelete("{usuarioId}"), Authorize]
        public IActionResult DeleteUsuario(Guid usuarioId)
        {
            string msg = Validation(usuarioId);
            if (string.IsNullOrEmpty(msg) == false)
                return Unauthorized(msg);

            var domain = new UsuarioDomain();
            bool retorno;
            try
            {
                if (domain.ObterUsuario(usuarioId) == null)
                    return Json(NotFound("Usuário não encontrado"));

                retorno =  domain.DeletarUsuario(usuarioId);
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            if (retorno != true)
                return Json(StatusCode(500, "Erro ao deletar usuário"));
            else
                return Json(Ok("Usuário deletado com sucesso")  );
        }

    }
}
