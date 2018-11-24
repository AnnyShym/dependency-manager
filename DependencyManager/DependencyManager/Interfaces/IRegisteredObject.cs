using System;

namespace DependencyManager.Interfaces
{
    public interface IRegisteredObject
    {
    
        Type ConcreteType { get; }
        IConfiguration Configuration { get; }

        object Instance { get; }

        void CreateInstance(object[] parameters);

    }
}
