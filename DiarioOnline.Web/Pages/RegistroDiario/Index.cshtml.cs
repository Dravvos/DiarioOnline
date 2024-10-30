using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DiarioOnline.Data.Context;
using DiarioOnline.Web.Services;
using DiarioOnline.DTO;
using System.Collections.Immutable;

namespace DiarioOnline.Web.Pages.RegistroDiario
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<RegistroDiarioDTO> RegistroDiario { get; set; } = default!;

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

        public IActionResult OnGet()
        {
            var user = SessionHelper.RecuperaSessao<string>(HttpContext, "usuario");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToPage("../Login");
            }
            string urlAPI = Services.Services.Api(_configuration, @"Usuario/GetUsuario/" + user);
            var usuario = BaseProxy<UsuarioDTO>.Get(HttpContext, urlAPI, true);

            var urlDiario = Services.Services.Api(_configuration, @"Diario/ObterDiarioPorUsuario/" + usuario!.Value!.Id.ToString());
            var diario = BaseProxy<DiarioDTO>.Get(HttpContext, urlDiario, true);

            string url = Services.Services.Api(_configuration, @"RegistroDiario/ObterRegistrosDiario/" + diario!.Value!.Id!.ToString());
            var res = BaseProxy<List<RegistroDiarioDTO>>.Get(HttpContext, url, true);

            RegistroDiario = res != null ? res.Value.OrderByDescending(x=>x.DataInclusao).ToList() : new List<RegistroDiarioDTO>();
            return Page();
        }

        public IActionResult OnPostDelete(Guid? id)
        {
            var user = SessionHelper.RecuperaSessao<string>(HttpContext, "usuario");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToPage("../Login");
            }
            string urlAPI = Services.Services.Api(_configuration, @"RegistroDiario/DeletarRegistroDiario/" + id);
            var res = BaseProxy<string>.Delete(HttpContext, urlAPI, true);
            
            return OnGet();

        }
    }
}
