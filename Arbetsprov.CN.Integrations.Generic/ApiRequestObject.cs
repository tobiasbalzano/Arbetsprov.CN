using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbetsprov.CN.Integrations.Generic
{
    public class ApiRequestObject<T>
    {
        public bool IsSuccess { get; set; }
        public T Content { get; set; }
    }
}
