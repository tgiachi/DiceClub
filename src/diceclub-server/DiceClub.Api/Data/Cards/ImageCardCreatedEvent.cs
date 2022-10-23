using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DiceClub.Api.Data.Cards
{
    public class ImageCardCreatedEvent : INotification
    {
        public string FileName { get; set; }
    }
}
