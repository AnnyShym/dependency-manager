using System;
using System.Collections.Generic;

namespace DependencyManager.Interfaces
{
    public interface IContainer
    {

        IDictionary<Type, IList<IRegisteredObject>> RegisteredObjects { get; }

        void Register<TConcreteType>(IConfiguration configuration)
            where TConcreteType : class;

        void Register<TTypeToResolve, TConcreteType>(IConfiguration configuration)
            where TTypeToResolve : class
            where TConcreteType : TTypeToResolve;

        TTypeToResolve Resolve<TTypeToResolve>(params IResolver[] resolvers)
            where TTypeToResolve : class;

    }
}
