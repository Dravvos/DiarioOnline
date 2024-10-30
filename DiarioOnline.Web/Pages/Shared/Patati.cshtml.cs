using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DiarioOnline.Data.Context;
using DiarioOnline.Data.Models;

namespace DiarioOnline.Web.Pages.Shared
{
    public class PatatiModel : PageModel
    {
        private readonly DiarioContext _context;

        public PatatiModel(DiarioContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Diario Diario { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Diario.Add(Diario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
