using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Cards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services
{
    public class TextExtractService : AbstractBaseService<TextExtractService>, INotificationHandler<ImageCardCreatedEvent>
    {
        public TextExtractService(IEventBusService eventBusService, ILogger<TextExtractService> logger) : base(eventBusService, logger)
        {
        }

        public Task Handle(ImageCardCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
