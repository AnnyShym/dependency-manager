using DependencyManager.Interfaces;
using System;

namespace DependencyManager.Classes
{
    public class ConfigurationResolver : IConfigurationResolver
    {

        public Type TypeToResolve { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public ConfigurationResolver(Type typeToResolve, IConfiguration configuration)
        {

            if (typeToResolve == null)
                throw new ArgumentNullException(Properties.Resources.ArgumentNullExceptionMessage);

            TypeToResolve = typeToResolve;
            Configuration = configuration;

        }

    }
}
