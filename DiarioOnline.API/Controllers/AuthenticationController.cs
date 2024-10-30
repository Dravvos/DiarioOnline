using DiarioOnline.BLL;
using DiarioOnline.Common;
using DiarioOnline.Data.Models;
using DiarioOnline.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiarioOnline.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost, AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                if (login == null)
                    return Json(BadRequest(ModelState));

                var domain = new UsuarioDomain();
                bool isLoginValid = domain.ValidaUsuario(login.Login, login.Password);

                if (isLoginValid)
                {
                    var authDomain = new AuthenticationDomain(_config);
                    var usuario = domain.ObterUsuario(login.Login);
                    if (usuario == null)
                        return Json(NotFound("Usuário não encontrado"));
                    usuario.Hash = "";
                    usuario.Salt = "";
                    var token = authDomain.GenerateToken(usuario);
                    return Json(Ok("Bearer " + token));
                }
                else
                    return Json(Unauthorized("Autenticação inválida, login ou senha incorretos"));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Signup([FromBody] UsuarioDTO dto)
        {
            var domain = new UsuarioDomain();
            try
            {
                if (dto == null)
                    return Json(BadRequest("Preencha todos os dados do usuário"));
                if (dto.Id.HasValue == false || dto.Id == Guid.Empty)
                    dto.Id = Guid.NewGuid();
                if (string.IsNullOrEmpty(dto.UserName))
                    return Json(BadRequest("Preencha o nome de usuário"));
                if (string.IsNullOrEmpty(dto.Hash))
                    return Json(BadRequest("Preencha a senha"));
                if (domain.ObterUsuario(dto.UserName) != null)
                    return Json(BadRequest("Este usuário já está cadastrado"));

                var user = Map<Usuario>.Convert(dto);
                domain.CriarUsuario(user);
                new DiarioDomain().CriarDiario(new Diario
                {
                    DataInclusao=DateTime.Now,
                    Id=Guid.NewGuid(),
                    UsuarioId = dto.Id.GetValueOrDefault()
                });
            }
            catch(ArgumentNullException ex)
            {
                return Json(BadRequest(ex.Message));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, "Algo deu errado na hora de salvar: " + ex.Message));
            }
            return Json(StatusCode(201, dto.Id));

        }
    }
}
