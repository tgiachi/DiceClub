using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Cards;

public class CardStageService : AbstractBaseService<CardStageService>
{
    public CardStageService(IEventBusService eventBusService, ILogger<CardStageService> logger) : base(eventBusService, logger)
    {
        
    }
}