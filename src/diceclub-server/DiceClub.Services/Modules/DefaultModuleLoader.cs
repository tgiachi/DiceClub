using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Autofac;
using DiceClub.Services.Cards;
using DiceClub.Services.Paginator;

namespace DiceClub.Services.Modules
{

    [ModuleLoader]
    public class DefaultModuleLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QueueService>().AsSelf().SingleInstance().AutoActivate();
            builder.RegisterType<RestPaginatorService>().AsSelf().SingleInstance();
            builder.RegisterType<CardStageService>().AsSelf().SingleInstance();
            base.Load(builder);
        }
    }
}
