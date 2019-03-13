using Ninject.Modules;
using STO.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STO.Business
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<SiparisContext>().To<SiparisContext>();
        }
    }
}
