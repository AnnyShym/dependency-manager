using DependencyManager.Types;

namespace DependencyManager.Interfaces
{
    public interface IContainer
    {

        void Register<TConcreteType>(LifeCycle lifeCycle)
            where TConcreteType : class;

        void Register<TTypeToResolve, TConcreteType>(LifeCycle lifeCycle)
            where TTypeToResolve : class
            where TConcreteType : TTypeToResolve;

        TTypeToResolve Resolve<TTypeToResolve>()
            where TTypeToResolve : class;

        TTypeToResolve Resolve<TTypeToResolve>(params IParameterResolver[] valueArgs)
            where TTypeToResolve : class;

    }
}
