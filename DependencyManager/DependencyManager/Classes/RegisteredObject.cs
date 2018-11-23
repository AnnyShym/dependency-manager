using System;
using DependencyManager.Types;
using DependencyManager.Interfaces;

namespace DependencyManager
{
    public class RegisteredObject : IRegisteredObject
    {

        public Type TypeToResolve { get; private set; }
        public Type ConcreteType { get; private set; }
        public LifeCycle LifeCycle { get; private set; }

        public object Instance { get; private set; }

        public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle)
        {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;            
        }

        public void CreateInstance(object[] parameters)
        {
            Instance = Activator.CreateInstance(ConcreteType, parameters);
        }

    }
}