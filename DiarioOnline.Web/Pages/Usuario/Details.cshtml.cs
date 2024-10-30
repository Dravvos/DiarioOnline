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

namespace DiarioOnline.Web.Pages.Usuario
{
    public class DetailsModel : PageModel
    {
        private readonly DiarioContext _context;

        public DetailsModel(DiarioContext context)
        {
            _context = context;
        }

        public UsuarioDTO Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var user = SessionHelper.RecuperaSessao<string>(HttpContext, "usuario");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToPage("../Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                Usuario = Map<UsuarioDTO>.Convert(usuario);
            }
            return Page();
        }
    }
}
