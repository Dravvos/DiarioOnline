using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioOnline.Common
{
    public class ObjetoRetornoAPI<T>
    {
        public T? Value { get; set; }
        public int StatusCode { get; set; }
        //contentTypes:[]
        //declaredType:null
        //formatters:[]
    }
}
