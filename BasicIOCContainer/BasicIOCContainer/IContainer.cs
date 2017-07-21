using System;

namespace BasicIocContainer
{
    public interface IContainer
    {
        void Register<TResolveFrom,TResolveTo>();
        T ResolveType<T>();
  
    }
}