using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Autofac;

namespace DiceClub.Services.Modules
{

    [ModuleLoader]
    public class DefaultServicesModuleLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookService>().AsSelf().SingleInstance();
            builder.RegisterType<InventoryService>().AsSelf().SingleInstance();

            base.Load(builder);
        }
    }
}
