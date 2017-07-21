using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicIocContainer
{
    public class BasicContainer : IContainer
    {
        private readonly Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();

        public void Register<TResolveFrom, TResolveto>() 
        {
            _registeredTypes.Add(typeof(TResolveFrom), typeof(TResolveto));
        }

        public T ResolveType<T>()
        {
            return (T) (Resolve(typeof(T)));
        }

        private object Resolve(Type typeToResolve)
        {
            Type resolvedType = null;

            if (typeToResolve.GetConstructors().Length == 1 &&
                typeToResolve.GetConstructors().First().GetParameters().Length == 0)
            {
                return Activator.CreateInstance(typeToResolve);
            }
            if (!_registeredTypes.ContainsKey(typeToResolve))
            {
                var exceptionMessage = $"Given Type {typeToResolve} is not Registered";
                throw new TypeNotRegisteredException(exceptionMessage);
            }
            resolvedType = _registeredTypes[typeToResolve];
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
