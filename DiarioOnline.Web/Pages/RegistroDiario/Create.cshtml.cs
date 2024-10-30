using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DiarioOnline.Data.Context;
using DiarioOnline.Data.Models;
using DiarioOnline.Web.Services;
using DiarioOnline.DTO;
using Newtonsoft.Json;

namespace DiarioOnline.Web.Pages.RegistroDiario
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            var user = SessionHelper.RecuperaSessao<string>(HttpContext, "usuario");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToPage("../Login");
            }
            RegistroDiario = new RegistroDiarioDTO();
            return Page();
        }

        [BindProperty]
        public RegistroDiarioDTO RegistroDiario { get; set; } = default!;

        private byte[]? ConvertImageToByte(IFormFile? file)
        {
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            else
                return null;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = SessionHelper.RecuperaSessao<string>(HttpContext, "usuario");
            var usuarioDTO = BaseProxy<UsuarioDTO>.Get(HttpContext, Services.Services.Api(_configuration, @"Usuario/GetUsuario/" + user), true);
            var diario = BaseProxy<DiarioDTO>.Get(HttpContext, Services.Services.Api(_configuration, @"Diario/ObterDiarioPorUsuario/" + usuarioDTO.Value.Id), true);
            RegistroDiario.DiarioId = diario.Value.Id.GetValueOrDefault();
            RegistroDiario.MidiaRegistroBytes = ConvertImageToByte(RegistroDiario.MidiaRegistro);
            RegistroDiario.MidiaRegistro = null;
            var res = BaseProxy<string>.Post(HttpContext, Services.Services.Api(_configuration, @"RegistroDiario/CriarRegistroDiario"),
                JsonConvert.SerializeObject(RegistroDiario), true);

            if (res.StatusCode == 200)
            {
                return RedirectToPage("../RegistroDiario/Index");
            }
            else if (res.StatusCode == 400)
            {
                ModelState.AddModelError("RegistroDiario.Registro", res.Value);
            }
            return Page();

        }
    }
}
