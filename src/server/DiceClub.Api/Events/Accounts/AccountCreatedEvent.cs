
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DiceClub.Api.Events.Accounts
{
    public class AccountCreatedEvent : INotification
    {
        public Guid UserId { get; set; }
    }
}
