using System;

namespace BasicIOCContainer
{
    internal interface IContainer
    {
        void Register<TResolveFrom,TResolveTo>();
        T ResolveType<T>();
  
    }
}