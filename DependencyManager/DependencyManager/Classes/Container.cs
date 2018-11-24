using DependencyManager.Exceptions;
using DependencyManager.Interfaces;
using DependencyManager.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyManager.Classes
{
    public class Container: IContainer
    {

        protected IDictionary<Type, ValueType[]> arguments;
        protected IDictionary<Type, IConfiguration> configurations;

        public IDictionary<Type, IList<IRegisteredObject>> RegisteredObjects { get; private set; }

        public Container()
        {
            RegisteredObjects = new Dictionary<Type, IList<IRegisteredObject>>();
        }

        public void Register<TConcreteType>(IConfiguration configuration)
            where TConcreteType: class
        {
            Register(typeof(TConcreteType), typeof(TConcreteType), configuration);
        }

        public void Register<TTypeToResolve, TConcreteType>(IConfiguration configuration)
            where TTypeToResolve : class
            where TConcreteType: TTypeToResolve
        {
            Register(typeof(TTypeToResolve), typeof(TConcreteType), configuration);
        }

        public TTypeToResolve Resolve<TTypeToResolve>(params IResolver[] resolvers)
            where TTypeToResolve : class
        {

            arguments = new Dictionary<Type, ValueType[]>();
            configurations = new Dictionary<Type, IConfiguration>();

            foreach (var resolver in resolvers)
                if (resolver is IParameterResolver)
                    arguments.Add(resolver.TypeToResolve, ((IParameterResolver)resolver).ValueArgs);
                else
                    configurations.Add(resolver.TypeToResolve, ((IConfigurationResolver)resolver).Configuration);

            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));

        }

        protected void Register(Type typeToResolve, Type concreteType, IConfiguration configuration)
        {

            if (!RegisteredObjects.ContainsKey(typeToResolve))
                RegisteredObjects.Add(typeToResolve, new List<IRegisteredObject>());

            if (configuration.LifeCycle == LifeCycle.Transient && RegisteredObjects[typeToResolve]
                .Where(o => (o.ConcreteType == concreteType && o.Configuration.LifeCycle == LifeCycle.Transient))
                .Any())
                throw new ArgumentException();

            RegisteredObjects[typeToResolve].Add(new RegisteredObject(concreteType, configuration));

        }

        protected object ResolveObject(Type typeToResolve)
        {

            IRegisteredObject registeredObject = null;
            IConfiguration configuration = configurations.ContainsKey(typeToResolve) ? configurations[typeToResolve] : new Configuration();
            if (typeToResolve.IsAbstract)
                registeredObject = RegisteredObjects[typeToResolve]
                    .FirstOrDefault(o => (o.Configuration.LifeCycle == configuration.LifeCycle && 
                    o.Configuration.Sticker == configuration.Sticker));
            else
                foreach (var keyValuePair in RegisteredObjects)
                    foreach (var obj in keyValuePair.Value)
                        if (obj.ConcreteType == typeToResolve && 
                            obj.Configuration.LifeCycle == configuration.LifeCycle &&
                            obj.Configuration.Sticker == configuration.Sticker) {
                            registeredObject = obj;
                            break;
                        }

            if (registeredObject == null)
                throw new TypeNotRegisteredException(Properties.Resources.TypeNotRegisteredExceptionMessage);

            return GetInstance(typeToResolve, registeredObject);

        }

        protected object GetInstance(Type typeToResolve, IRegisteredObject registeredObject)
        {

            if (registeredObject.Instance == null ||
                registeredObject.Configuration.LifeCycle == LifeCycle.Transient) {
                IEnumerable<object> parameters = ResolveConstructorParameters(typeToResolve, registeredObject);
                registeredObject.CreateInstance(parameters.ToArray());
            }

            return registeredObject.Instance;

        }

        protected IEnumerable<object> ResolveConstructorParameters(Type typeToResolve, IRegisteredObject registeredObject)
        {

            ConstructorInfo constructorInfo = registeredObject.ConcreteType
                .GetConstructors()
                .ToList()
                .OrderByDescending(elem => elem.GetParameters().Count())
                .FirstOrDefault() ?? throw new ArgumentNullException();

            int i = 0;
            foreach (var parameter in constructorInfo.GetParameters())
                if (parameter.GetType().IsValueType)
                    yield return arguments[typeToResolve][i++];
                else
                    yield return ResolveObject(parameter.ParameterType);

        }

    }
}
