using DiarioOnline.BLL;
using DiarioOnline.Common;
using DiarioOnline.Data.Models;
using DiarioOnline.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DiarioOnline.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DiarioController : Controller
    {
        [HttpGet("{usuarioId}"), Authorize]
        public IActionResult ObterDiarioPorUsuario(Guid usuarioId)
        {
            var domain = new DiarioDomain();
            try
            {
                var diario = domain.ObterDiarioPorUsuario(usuarioId);
                if (diario == null)
                    return Json(NotFound());

                return Json(Ok(diario));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
        }
        [HttpGet("{diarioId}"), Authorize]
        public IActionResult ObterDiario(Guid diarioId)
        {
            var domain = new DiarioDomain();
            try
            {
                var diario = domain.ObterDiario(diarioId);
                if (diario == null)
                    return Json(NotFound());

                return Json(Ok(diario));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
        }

        [HttpPut,Authorize]
        public IActionResult AtualizarDiario([FromBody] DiarioDTO diarioUpdate)
        {

            if (diarioUpdate == null)
                return Json(BadRequest(ModelState));

            if (diarioUpdate.Id == null || diarioUpdate.Id == Guid.Empty)
                return Json(BadRequest("Preencha o Id do Diário"));

            bool retorno;
            var domain = new DiarioDomain();
            var diario = Map<Diario>.Convert(diarioUpdate);
            try
            {
                if (domain.ObterDiario(diarioUpdate.Id.Value) == null)
                    return Json(NotFound("Diário não encontrado"));

                retorno = domain.AtualizarDiario(diario);
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            if (retorno == false)
                return Json(StatusCode(500, "Não foi possível atualizar o diário"));
            else
                return Json(Ok("Diário atualizado com sucesso"));
        }

        [HttpPost, Authorize]
        public IActionResult CriarDiario([FromBody] DiarioDTO diarioCreate)
        {
            if (diarioCreate == null)
                return Json(BadRequest(ModelState));

            if (diarioCreate.Id == null || diarioCreate.Id == Guid.Empty)
                diarioCreate.Id = Guid.NewGuid();

            bool retorno;
            var domain = new DiarioDomain();
            try
            {
                if (domain.ObterDiario(diarioCreate.Id.GetValueOrDefault()) != null)
                    return Json(BadRequest("Não é possível criar um diário com um id já existente."));

                var diario = Map<Diario>.Convert(diarioCreate);
                retorno = domain.CriarDiario(diario);
            }
            catch (ArgumentNullException ex)
            {
                return Json(BadRequest(ex.Message));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            if (retorno)
                return Json(Ok(diarioCreate.Id));
            else
                return Json(StatusCode(500, "Não foi possível criar um diário"));
        }

        [HttpDelete("{diarioId}"), Authorize]
        public IActionResult DeleteDiario(Guid diarioId)
        {
            var domain = new DiarioDomain();
            bool retorno;
            try
            {
                if (domain.ObterDiario(diarioId) == null)
                    return Json(NotFound("Diário não encontrado"));

                retorno = domain.DeletarDiario(diarioId);
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            if (retorno != true)
                return Json(StatusCode(500, "Erro ao deletar diário"));
            else
                return Json(Ok("diário deletado com sucesso"));
        }
    }
}
