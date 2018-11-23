using DependencyManager.Exceptions;
using DependencyManager.Interfaces;
using DependencyManager.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyManager
{
    public class Container: IContainer
    {

        public List<RegisteredObject> RegisteredObjects { get; private set; }

        public Container()
        {
            RegisteredObjects = new List<RegisteredObject>();
        }

        public void Register<TConcreteType>(LifeCycle lifeCycle)
            where TConcreteType: class
        {
            RegisteredObjects.Add(new RegisteredObject(null, typeof(TConcreteType), lifeCycle));
        }

        public void Register<TTypeToResolve, TConcreteType>(LifeCycle lifeCycle)
            where TTypeToResolve : class
            where TConcreteType: TTypeToResolve
        {
            RegisteredObjects.Add(new RegisteredObject(typeof(TTypeToResolve), typeof(TConcreteType), lifeCycle));
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
            where TTypeToResolve: class
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve), null);
        }

        public TTypeToResolve Resolve<TTypeToResolve>(params IParameterResolver[] valueArgs)
            where TTypeToResolve : class
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve), valueArgs);
        }

        private object ResolveObject(Type typeToResolve, IParameterResolver[] valueArgs)
        {

            RegisteredObject registeredObject;
            if (typeToResolve.IsAbstract)
                registeredObject = RegisteredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            else
                registeredObject = RegisteredObjects.FirstOrDefault(o => o.ConcreteType == typeToResolve);

            if (registeredObject == null)
                throw new TypeNotRegisteredException(Properties.Resources.TypeNotRegisteredExceptionMessage);

            return GetInstance(registeredObject, valueArgs);

        }

        private object GetInstance(RegisteredObject registeredObject, IParameterResolver[] valueArgs)
        {

            if (registeredObject.Instance == null ||
                registeredObject.LifeCycle == LifeCycle.Transient) {
                IEnumerable<object> parameters = ResolveConstructorParameters(registeredObject, valueArgs);
                registeredObject.CreateInstance(parameters.ToArray());
            }

            return registeredObject.Instance;

        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject, IParameterResolver[] valueArgs)
        {

            ConstructorInfo[] constructorsInfo = registeredObject.ConcreteType.GetConstructors();

            int indMax = 0;
            int maxLength = 0;
            for (int i = 0; i < constructorsInfo.Length; i++) {

                int currentLength = constructorsInfo[i].GetParameters().Length;
                if (currentLength > maxLength) {
                    indMax = i;
                    maxLength = currentLength;            
                }

            }

            //ConstructorInfo constructorInfo = constructorsInfo[indMax];   
            //foreach (var parameter in constructorInfo.GetParameters())
            //    yield return ResolveObject(parameter.ParameterType);

        }

    }
}
