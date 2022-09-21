using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using EasyNetQ;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services
{
    public class QueueService : AbstractBaseService<QueueService>
    {
        private readonly IBus _bus;
        public QueueService(IEventBusService eventBusService, ILogger<QueueService> logger, IBus bus) : base(eventBusService, logger)
        {
            _bus = bus;

        }

        public Task PublishToQueue<TData>(string queueName, TData data)
        {
            return _bus.SendReceive.SendAsync(queueName, data);
        }

        public async Task SubscribeToQueue<TData>(string queueName, Action<TData> action)
        {
            await _bus.SendReceive.ReceiveAsync(queueName, action);
        }

    }

}
