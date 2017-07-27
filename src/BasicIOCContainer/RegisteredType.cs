using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BasicIocContainer
{
    class RegisteredType
    {
       public RegisteredType(Type typetoResolve,Type concreteType,LifeCycle lifeCycle)
        {
            TypetoResolve = typetoResolve;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;
        }

        public object Instance { get; set; }
        public Type TypetoResolve { get; set; }
        public Type ConcreteType { get; set; }

        public override string ToString()
        {
            return TypetoResolve.FullName+":"+ConcreteType.FullName;
        }

        public LifeCycle LifeCycle { get; set; }

        public void CreateInstance(object[] constructorParameters)
        {
            this.Instance = Activator.CreateInstance(ConcreteType, constructorParameters);
        }
       
    }
}
