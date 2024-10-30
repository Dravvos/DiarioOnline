using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DiarioOnline.Common;
using DiarioOnline.DTO;
using DiarioOnline.BLL;
using DiarioOnline.Data.Models;

namespace DiarioOnline.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RegistroDiarioController : Controller
    {
        [HttpGet("{registroDiarioId:Guid}"), Authorize]
        public IActionResult ObterRegistroDiario(Guid registroDiarioId)
        {
            var domain = new RegistroDiarioDomain();
            try
            {
                var registroDiario = domain.ObterRegistroDiario(registroDiarioId);
                if (registroDiario == null)
                    return Json(NotFound());

                return Json(Ok(registroDiario));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
        }
        [HttpGet("{diarioId}"), Authorize]
        public IActionResult ObterRegistrosDiario(Guid diarioId)
        {
            var domain = new RegistroDiarioDomain();
            try
            {
                var registroDiario = domain.ObterRegistrosDiario(diarioId);
                if (registroDiario == null)
                    return Json(NotFound());

                return Json(Ok(registroDiario));
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
        }

        [HttpPut, Authorize]
        public IActionResult AtualizarRegistroDiario([FromBody] RegistroDiarioDTO registroDiarioUpdate)
        {

            if (registroDiarioUpdate == null)
                return Json(BadRequest(ModelState));

            if (registroDiarioUpdate.Id == null || registroDiarioUpdate.Id == Guid.Empty)
                return Json(BadRequest("Preencha o Id do Diário"));

            bool retorno;
            var domain = new RegistroDiarioDomain();
            var registroDiario = new RegistroDiario
            {
                Id = registroDiarioUpdate.Id.GetValueOrDefault(),
                DataInclusao = registroDiarioUpdate.DataInclusao,
                DiarioId = registroDiarioUpdate.DiarioId,
                MidiaRegistro = registroDiarioUpdate.MidiaRegistroBytes,
                Registro = registroDiarioUpdate.Registro,
            };

            try
            {
                var registro = domain.ObterRegistroDiario(registroDiarioUpdate.Id.Value);
                if (registro == null)
                    return Json(NotFound("Diário não encontrado"));
                registroDiario.DataInclusao = registro.DataInclusao;
                retorno = domain.AtualizarRegistroDiario(registroDiario);
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            if (retorno == false)
                return Json(StatusCode(500, "Não foi possível atualizar o registro do diário"));
            else
                return Json(Ok("Registro de diário atualizado com sucesso"));
        }

        [HttpPost, Authorize]
        public IActionResult CriarRegistroDiario([FromBody] RegistroDiarioDTO registroDiarioCreate)
        {
            if (registroDiarioCreate == null)
                return Json(BadRequest(ModelState));

            if (registroDiarioCreate.Id == null || registroDiarioCreate.Id == Guid.Empty)
                registroDiarioCreate.Id = Guid.NewGuid();

            bool retorno;
            var domain = new RegistroDiarioDomain();
            try
            {
                var registroDiario = new RegistroDiario
                {
                    Id = registroDiarioCreate.Id.GetValueOrDefault(),
                    DiarioId = registroDiarioCreate.DiarioId,
                    DataInclusao = DateTime.Now,
                    Registro = registroDiarioCreate.Registro,
                    MidiaRegistro = registroDiarioCreate.MidiaRegistroBytes,
                };
                retorno = domain.CriarRegistroDiario(registroDiario);
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
                return Json(Ok(registroDiarioCreate.Id));
            else
                return Json(StatusCode(500, "Não foi possível criar um registro do diário"));
        }

        [HttpDelete("{registroDiarioId}"), Authorize]
        public IActionResult DeletarRegistroDiario(Guid registroDiarioId)
        {
            var domain = new RegistroDiarioDomain();
            bool retorno;
            try
            {
                if (domain.ObterRegistroDiario(registroDiarioId) == null)
                    return Json(NotFound("Registro do diário não encontrado"));

                retorno = domain.DeletarRegistroDiario(registroDiarioId);
            }
            catch (Exception ex)
            {
                return Json(StatusCode(500, ex.Message));
            }
            if (retorno != true)
                return Json(StatusCode(500, "Erro ao deletar registro do diário"));
            else
                return Json(Ok("Registro do diário deletado com sucesso"));
        }
    }
}
