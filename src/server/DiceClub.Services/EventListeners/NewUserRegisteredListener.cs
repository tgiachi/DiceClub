using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Events.Accounts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.EventListeners
{
    public class NewUserRegisteredListener : AbstractBaseService<NewUserRegisteredListener>, INotificationHandler<AccountCreatedEvent>
    {
        public NewUserRegisteredListener(IEventBusService eventBusService, ILogger<NewUserRegisteredListener> logger) : base(eventBusService, logger)
        {
        }

        public Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
