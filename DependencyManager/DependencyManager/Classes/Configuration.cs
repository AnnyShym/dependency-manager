using DependencyManager.Interfaces;
using DependencyManager.Types;

namespace DependencyManager.Classes
{
    public class Configuration : IConfiguration
    {

        public LifeCycle LifeCycle { get; private set; }
        public string Sticker { get; private set; }

        public Configuration(LifeCycle lifeCycle = LifeCycle.Transient, string sticker = null)
        {
            LifeCycle = lifeCycle;
            Sticker = sticker;
        }

    }
}
