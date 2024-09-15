using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    interface IValidationService<T> where T : class
    {
        public List<string> IsValid(T Entity);
    }
}
