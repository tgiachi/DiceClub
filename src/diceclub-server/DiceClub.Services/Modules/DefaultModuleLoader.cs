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
    public class DefaultModuleLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QueueService>().AsSelf().SingleInstance().AutoActivate();
            base.Load(builder);
        }
    }
}
