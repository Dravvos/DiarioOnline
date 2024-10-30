using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web;

namespace DiarioOnline.Web.Attributes
{
    public class LoggedInAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("DiarioToken"))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                // Lógica para validar o cookie e obter informações do usuário
                // ...
            }
        }
    }
}
