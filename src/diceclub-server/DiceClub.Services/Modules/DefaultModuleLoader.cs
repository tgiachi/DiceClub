using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Autofac;
using DiceClub.Services.Cards;
using DiceClub.Services.Inventory;
using DiceClub.Services.Paginator;

namespace DiceClub.Services.Modules
{

    [ModuleLoader]
    public class DefaultModuleLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
          //  builder.RegisterType<QueueService>().AsSelf().SingleInstance().AutoActivate();
            builder.RegisterType<RestPaginatorService>().AsSelf().SingleInstance();
            builder.RegisterType<CardStageService>().AsSelf().SingleInstance();
            builder.RegisterType<ExportCardService>().AsSelf().SingleInstance();
            builder.RegisterType<InventoryService>().AsSelf().SingleInstance();
            //builder.RegisterType<TextExtractService>().AsSelf().SingleInstance().AutoActivate();
            //builder.RegisterType<DirectoryWatcherService>().AsSelf().SingleInstance().AutoActivate();
            base.Load(builder);
        }
    }
}
