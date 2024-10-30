using DiarioOnline.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DiarioOnline.Data
{
    public class BaseDal
    {        
        private DiarioContext? context { get; set; }
        public DiarioContext Context
        {
            get
            {
                var context = new DiarioContext();
                context.Database.SetCommandTimeout(600);
                return context;
            }
        }
        public BaseDal(DiarioContext? con = null)
        {
            context = con;
            if (context != null && context.Database != null)
                context.Database.SetCommandTimeout(600);
        }
        public DiarioContext GetContext()
        {
            return Context;
        }
    }
}
