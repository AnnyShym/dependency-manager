using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyManager.Classes
{
    public class SimpleControllerFactory : DefaultControllerFactory
    {

        private readonly IContainer container;


        public SimpleIocControllerFactory(IContainer container)
        {
            this.container = container;
        }


        protected override IController GetControllerInstance(Type controllerType)
        {
            return container.Resolve(controllerType) as Controller;
        }

    }
}
