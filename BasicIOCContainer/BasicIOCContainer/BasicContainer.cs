using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicIOCContainer
{
    class BasicContainer : IContainer
    {
        Dictionary<Type,Type> registeredTypes=new Dictionary<Type, Type>();

        public void Register<TResolveFrom,TResolveto>()
        {
            registeredTypes.Add(typeof(TResolveFrom),typeof(TResolveto));
        }

        public T ResolveType<T>()
        {
            return (T) (Resolve(typeof(T)));
        }

        private object Resolve(Type typeToResolve)
        {
            Type resolvedType = null;
            try
            {
                resolvedType = registeredTypes[typeToResolve];
            }
            catch (Exception e)
            {
                Console.WriteLine("This type {0} is not registered :"+ typeToResolve.FullName);
            }

            return ResolveConstructorParameters(resolvedType);
          
           }

        private object ResolveConstructorParameters(Type resolvedType)
        {

            var firstConstructor = resolvedType.GetConstructors().First();
            if (firstConstructor.GetParameters().Length == 0)
            {
                return Activator.CreateInstance(resolvedType);
            }
            var constructorParameters = firstConstructor.GetParameters();
            return firstConstructor.Invoke(GetInstanceFor(constructorParameters));
        }

        private object[] GetInstanceFor(ParameterInfo[] constructorParameters)
        {

            IList<Object> parameters = new List<object>();
            foreach (var parameter in constructorParameters)
            {
                parameters.Add(Resolve(parameter.ParameterType));
            }

            return parameters.ToArray();
        }
    }
}
