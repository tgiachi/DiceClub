using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Autofac;
using DiceClub.Services.EventListeners;

namespace DiceClub.Services.Modules
{
    [ModuleLoader]
    public class EventListenerModuleLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewUserRegisteredListener>().AsSelf().SingleInstance();
            base.Load(builder);
        }
    }
}
