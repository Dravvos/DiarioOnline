using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiarioOnline.Data.Context;
using DiarioOnline.Web.Attributes;
using DiarioOnline.DTO;
using DiarioOnline.Common;
using DiarioOnline.Web.Services;

namespace DiarioOnline.Web.Pages.Usuario
{
    public class EditModel : PageModel
    {
        private readonly DiarioContext _context;

        public EditModel(DiarioContext context)
        {
            _context = context;
        }

        [BindProperty]
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

            var usuario =  await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            Usuario = Map<UsuarioDTO>.Convert(usuario);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(Usuario.Id.GetValueOrDefault()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
