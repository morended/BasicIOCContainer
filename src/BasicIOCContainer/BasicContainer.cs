using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BasicIocContainer
{
    public class BasicContainer : IContainer
    {
        private readonly IDictionary<Type, RegisteredType> _registeredObjects;

        public BasicContainer()
        {
            _registeredObjects= new Dictionary<Type, RegisteredType>();
        }

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
            var typeOfResolveFrom = typeof(TResolveFrom);
            var typeOfResolveTo = typeof(TResolveto);
            var registeredType = new RegisteredType(typeOfResolveFrom, typeOfResolveTo, lifeCycle);

            if (_registeredObjects.ContainsKey(typeOfResolveFrom))
                _registeredObjects[typeOfResolveFrom] = registeredType;
            else
                _registeredObjects.Add(typeOfResolveFrom, registeredType);
        }

        public T ResolveType<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type typeToResolve)
        {
            if (typeToResolve == null)
            {
                return null;
            }

            RegisteredType registeredType = GetRegisterType(typeToResolve);

            if (registeredType == null)
            {
                throw new TypeNotRegisteredException(typeToResolve?.FullName);
            }

            var isSingletonAndIsInstanceCreated = registeredType.LifeCycle == LifeCycle.Singleton &&
                                                  registeredType.Instance != null;
            return isSingletonAndIsInstanceCreated ? registeredType.Instance : ResolveConstructorParameters(registeredType);
        }

        private RegisteredType GetRegisterType(Type typeToResolve)
        {
            RegisteredType registeredType = null;

            if (_registeredObjects.ContainsKey(typeToResolve))
            {
                registeredType = _registeredObjects[typeToResolve];
            }

            else if (!typeToResolve.IsInterface)
            {
                registeredType = new RegisteredType(typeToResolve, typeToResolve, LifeCycle.Transient);
                _registeredObjects.Add(typeToResolve, registeredType);
            }

            return registeredType;
        }

        private object ResolveConstructorParameters(RegisteredType registeredType)
        {
            try
            {
                var constructor = registeredType
                    .ConcreteType
                    .GetConstructors()
                    .OrderByDescending(x => x.GetParameters().Length)
                    .First();
                var constructorParameterInstances = GetInstancesFor(constructor.GetParameters());
                registeredType.CreateInstance(constructorParameterInstances);
            }
            catch (ConstructorNotFoundException)
            {
                throw new ConstructorNotFoundException(registeredType.ToString());
            }
            return registeredType.Instance;
        }

        private object[] GetInstancesFor(ParameterInfo[] constructorParameters)
        {
           IList<object> parameters =  constructorParameters
                                       .Select(parameter => parameter.ParameterType.IsPrimitive
                                       ? parameter.ParameterType.TypeInitializer
                                       : Resolve(parameter.ParameterType))
                                       .ToList();
            return parameters.ToArray();
                
        }
    }
}