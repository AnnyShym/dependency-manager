using System;

namespace DependencyManager.Interfaces
{
    public interface IConfigurationResolver : IResolver
    {
        IConfiguration Configuration { get; }
    }
}
