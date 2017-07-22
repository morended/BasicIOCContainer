using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BasicIocContainer
{
    using System;
  
    public class TypeNotRegisteredException : Exception
    {   
        public TypeNotRegisteredException(String message):base(GenerateMessage(message))
        {
          
        }
       
        private static string GenerateMessage(Object message)
        {
            return $"Type not Registered. [{message}]";
        }
    }
}
