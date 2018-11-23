namespace DependencyManager.Interfaces
{
    public interface IRegisteredObject
    {

        object Instance { get; }

        void CreateInstance(object[] parameters);

    }
}
