using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BasicIocContainer
{
    public class BasicContainer : IContainer
    {
        private readonly IDictionary<Type, RegisteredType> _registeredObjects = new Dictionary<Type, RegisteredType>();

        public void Register<TResolveFrom, TResolveto>()
            where TResolveto : class 
            where TResolveFrom:class 
        {
            Register<TResolveFrom, TResolveto>(LifeCycle.Transient);
        }

        public void Register<TResolveFrom, TResolveto>(LifeCycle lifeCycle) 
            where TResolveto : class 
            where TResolveFrom:class
        {
            var registeredType = new RegisteredType(typeof(TResolveFrom), typeof(TResolveto), lifeCycle);

            if (_registeredObjects.ContainsKey(typeof(TResolveFrom)))
                _registeredObjects[typeof(TResolveFrom)] = registeredType;
            else
                _registeredObjects.Add(typeof(TResolveFrom), registeredType);
        }

        public T ResolveType<T>()
        {
            return (T) Resolve(typeof(T));
        }

        private object Resolve(Type typeToResolve)
        {
           
           RegisteredType registeredType = ConstructRegisterType(typeToResolve);
            if (registeredType == null)
            {
                throw new TypeNotRegisteredException(typeToResolve?.FullName);
            }

            if (registeredType.LifeCycle == LifeCycle.Singleton && registeredType.Instance != null)
                return registeredType.Instance;

            return ResolveConstructorParameters(registeredType);
        }

        private RegisteredType ConstructRegisterType(Type typeToResolve)
        {
            RegisteredType registeredType = null;
            if (_registeredObjects.ContainsKey(typeToResolve))
            {
                registeredType = _registeredObjects[typeToResolve];
            }
            else if (typeToResolve != null && !typeToResolve.IsInterface)
            {
                registeredType = new RegisteredType(typeToResolve, typeToResolve, LifeCycle.Transient);
                _registeredObjects.Add(typeToResolve, registeredType);
            }
            return registeredType;
        }

        private object ResolveConstructorParameters(RegisteredType registeredType)
        {
            var firstConstructor = registeredType.ConcreteType.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length).First();
            var constructorParameters = GetInstanceFor(firstConstructor.GetParameters());
            registeredType.CreateInstance(constructorParameters);
            return registeredType.Instance;
        }

        private object[] GetInstanceFor(ParameterInfo[] constructorParameters)
        {
            IList<object> parameters = constructorParameters.Select(parameter => parameter.ParameterType.IsPrimitive
                    ? parameter.ParameterType.TypeInitializer
                    :Resolve(parameter.ParameterType))
                .ToList();
            return parameters.ToArray();
        }
    }
}