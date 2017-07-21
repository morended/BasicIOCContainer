using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIocContainer
{
    using System;

    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException()
        {
        }
        public TypeNotRegisteredException(string message)
            : base(message)
        {
         }

        public TypeNotRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
