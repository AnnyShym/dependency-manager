using DependencyManager.Interfaces;
using System;
using System.Reflection;

namespace DependencyManager.Classes
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

        public void ResolveProperties(PropertyInfo property, object propertyValue)
        {
            property.SetValue(Instance, propertyValue);
        }

    }
}