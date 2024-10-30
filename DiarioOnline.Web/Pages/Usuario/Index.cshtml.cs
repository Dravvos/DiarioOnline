using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DiarioOnline.Data.Context;
using DiarioOnline.DTO;
using DiarioOnline.Common;
using DiarioOnline.Web.Services;
using Newtonsoft.Json;
using System.Security.Policy;

namespace DiarioOnline.Web.Pages.Usuario
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public UsuarioDTO? Usuario { get; set; } = default!;

        public IActionResult OnGet()
        {
            var user = SessionHelper.RecuperaSessao<string>(HttpContext, "usuario");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToPage("../Login");
            } 
            ModelState.Clear();
            var res = BaseProxy<UsuarioDTO>.Get(HttpContext, Services.Services.Api(_configuration, @"Usuario/GetUsuario/" + user), true);
            Usuario = res!.Value;
            return Page();
        }
    }
}
