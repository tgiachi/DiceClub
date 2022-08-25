using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DiceClub.Api.Events.Inventory
{
    public class InventoryItemAddedEvent : INotification
    {
        public Guid Id { get; set; }
    }
}
