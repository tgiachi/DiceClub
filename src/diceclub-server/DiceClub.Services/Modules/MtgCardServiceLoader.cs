using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Autofac;
using DiceClub.Services.Cards;
using MtgApiManager.Lib.Service;
using ScryfallApi.Client;

namespace DiceClub.Services.Modules
{

    [ModuleLoader]
    public class MtgCardServiceLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(s => new ScryfallApiClient(
                new HttpClient() { BaseAddress = new Uri("https://api.scryfall.com/") },
                ScryfallApiClientConfig.GetDefault())).AsSelf();

            builder.RegisterType<MtgServiceProvider>().AsSelf().SingleInstance();
            builder.RegisterType<MtgCardService>().AsSelf().SingleInstance();
            builder.RegisterType<CardService>().AsSelf().SingleInstance();

            base.Load(builder);
        }
    }
}
