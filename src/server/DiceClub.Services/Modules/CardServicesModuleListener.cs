using Aurora.Api.Attributes;
using Autofac;
using DiceClub.Services.Cards;
using MtgApiManager.Lib.Service;
using ScryfallApi.Client;

namespace DiceClub.Services.Modules;


[ModuleLoader]
public class CardServicesModuleListener : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ImportService>().AsSelf().SingleInstance();
        builder.RegisterType<CardCastleCsvParser>().AsSelf().SingleInstance();
        
        builder.Register(s => new ScryfallApiClient(
            new HttpClient() { BaseAddress = new Uri("https://api.scryfall.com/") },
            ScryfallApiClientConfig.GetDefault())).AsSelf();

        builder.RegisterType<MtgServiceProvider>().AsSelf().SingleInstance();
        builder.RegisterType<ImportMtgService>().AsSelf().SingleInstance();
        
        base.Load(builder);
    }
}