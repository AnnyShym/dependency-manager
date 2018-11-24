using DependencyManager.Interfaces;
using System;

namespace DependencyManager.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyInjectionAttribute: Attribute, IPropertyInjectionAttribute
    {        
    }
}
