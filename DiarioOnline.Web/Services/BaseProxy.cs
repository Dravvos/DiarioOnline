using DiarioOnline.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DiarioOnline.Web.Services
{
    public static class BaseProxy<T>
    {
        public static ObjetoRetornoAPI<T>? Get(HttpContext context, string service, bool authorize)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (authorize)
                {
                    var usuario = SessionHelper.RecuperaSessao<string>(context,"usuario");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionHelper.RecuperaSessao<string>(context, usuario));
                }
                var response = client.GetAsync(service).Result;
                using (HttpContent content = response.Content)
                {
                    var resultJson = content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ObjetoRetornoAPI<T>>(resultJson);
                    return result;
                }
            }
        }
        public static ObjetoRetornoAPI<T>? Post(HttpContext context,string service, string content, bool authorize)
        {
            using (var client = new HttpClient())
            {

                client.Timeout = TimeSpan.FromMinutes(30);
                var stringContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
                if (authorize)
                {
                    var usuario = SessionHelper.RecuperaSessao<string>(context, "usuario");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionHelper.RecuperaSessao<string>(context, usuario));
                }
                var postTask = client.PostAsync(service, stringContent);
                postTask.Wait();
                var resultJson = postTask.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ObjetoRetornoAPI<T>>(resultJson);
                return result;                
            }
        }
        public static ObjetoRetornoAPI<T>? Put(HttpContext context, string service, string content, bool authorize)
        {
            using (var client = new HttpClient())
            {

                client.Timeout = TimeSpan.FromMinutes(30);
                var stringContent = new StringContent(content, System.Text.Encoding.UTF8, "application/json");
                if (authorize)
                {
                    var usuario = SessionHelper.RecuperaSessao<string>(context, "usuario");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionHelper.RecuperaSessao<string>(context, usuario));
                }
                var putTask = client.PutAsync(service, stringContent);
                putTask.Wait();
                var resultJson = putTask.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ObjetoRetornoAPI<T>>(resultJson);
                return result;
            }
        }

        public static ObjetoRetornoAPI<T>? Delete(HttpContext context, string service, bool authorize)
        {
            using (var client = new HttpClient())
            {

                client.Timeout = TimeSpan.FromMinutes(30);
                if (authorize)
                {
                    var usuario = SessionHelper.RecuperaSessao<string>(context, "usuario");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionHelper.RecuperaSessao<string>(context, usuario));
                }
                var postTask = client.DeleteAsync(service);
                postTask.Wait();
                var resultJson = postTask.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ObjetoRetornoAPI<T>>(resultJson);
                return result;
            }
        }


    }
}
