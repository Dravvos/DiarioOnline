using DiarioOnline.DTO;
using DiarioOnline.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;

namespace DiarioOnline.Web.Pages
{
    public class SignupModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public SignupModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }        

        [BindProperty]
        public UsuarioDTO Usuario { get; set; } = default!;
        
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var res = BaseProxy<string>.Post(HttpContext, Services.Services.Api(_configuration, @"Authentication/Signup"), JsonConvert.SerializeObject(Usuario), false);
            if (res.StatusCode == 200)
            {
                return RedirectToPage("Login");
            }
            else if (res.StatusCode == 400)
            {
                ModelState.AddModelError(res.StatusCode.ToString(), res.Value);
            }
            return Page();


        }
    }
}
