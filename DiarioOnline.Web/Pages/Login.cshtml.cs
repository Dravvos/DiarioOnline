using DiarioOnline.DTO;
using DiarioOnline.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace DiarioOnline.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginDTO User { get; set; } = default!;

        [BindProperty]
        public string Login { get; set; }
        //statusCode:int
        //value: T
        //contentTypes:[]
        //declaredType:null
        //formatters:[]
        public IActionResult OnPost()
        {
            ModelState.Clear();
            var res = BaseProxy<string>.Post(HttpContext, Services.Services.Api(_configuration, @"Authentication/Login"), JsonConvert.SerializeObject(User), false);
            
            if (res.StatusCode == 200)
            {
                SessionHelper.SalvarSessao(HttpContext, "usuario", User.Login);
                SessionHelper.SalvarSessao(HttpContext, User.Login, res.Value.Split(' ')[1]);
                SessionHelper.SalvarSessao(HttpContext, "Logado", true);
                TempData["Logado"] = true;
                return RedirectToPage("RegistroDiario/Index");
            }
            else if (res.StatusCode == 401)
            {
                ModelState.AddModelError("Login", res.Value);
            }
            else if (res.StatusCode == 400)
            {
                ModelState.AddModelError("Login", res.Value);                
            }
            
            return Page();


        }
    }
}
