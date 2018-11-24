using DependencyManager.Interfaces;
using System;

namespace DependencyManager
{
    public class RegisteredObject : IRegisteredObject
    {

        public Type ConcreteType { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public object Instance { get; private set; }

        public RegisteredObject(Type concreteType, IConfiguration configuration)
        {

            if (concreteType == null)
                throw new ArgumentNullException(Properties.Resources.ArgumentNullExceptionMessage);

            ConcreteType = concreteType;
            Configuration = configuration;
   
        }

        public void CreateInstance(object[] parameters)
        {
            Instance = Activator.CreateInstance(ConcreteType, parameters);
        }

    }
}