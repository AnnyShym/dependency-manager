using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyManager.Classes
{
    class Example
    {
        protected void Application_Start()
        {

            var container = new SimpleIocContainer();


            BootStrapper.Configure(container);


            ControllerBuilder.Current.SetControllerFactory(new SimpleIocControllerFactory(container));

        }
    }
}
