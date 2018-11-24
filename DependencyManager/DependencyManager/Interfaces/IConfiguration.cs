using DependencyManager.Types;

namespace DependencyManager.Interfaces
{
    public interface IConfiguration
    {
        LifeCycle LifeCycle { get; }
        string Sticker { get; }        
    }
}
