using System;

namespace BasicIocContainer
{
    public interface IContainer
    {
        void Register<TResolveFrom, TResolveTo>() where TResolveFrom : class where TResolveTo : class;
        void Register<TResolveFrom, TResolveTo>(LifeCycle lifeCycle) where TResolveTo : class where TResolveFrom:class;
        T ResolveType<T>();
        object Resolve(Type typeToResolve);

    }
}