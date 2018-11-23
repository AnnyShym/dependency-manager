using System;

namespace DependencyManager.Exceptions
{
    public class TypeNotRegisteredException : ArgumentException
    {
        public TypeNotRegisteredException() { }
        public TypeNotRegisteredException(string message) : base(message) { }
    }
}
