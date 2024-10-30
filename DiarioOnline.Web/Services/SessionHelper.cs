using DiarioOnline.Data.Models;
using Newtonsoft.Json;

namespace DiarioOnline.Web.Services
{
    public static class SessionHelper
    {
        public static void SalvarSessao<T>(HttpContext context, string chave, T valor)
        {
            var ValorSerializado = JsonConvert.SerializeObject(valor);
            context.Session.SetString(chave, ValorSerializado);
        }
        public static T? RecuperaSessao<T>(HttpContext contexto, string chave)
        {
            var ValorEmCache = contexto.Session.GetString(chave);
            if (ValorEmCache == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(ValorEmCache);
        }
    }
}
