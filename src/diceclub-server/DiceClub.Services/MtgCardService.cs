using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Database.Dao.Cards;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services
{
    public class MtgCardService : AbstractBaseService<MtgCardService>
    {
        private readonly MtgDumpDao _mtgDumpDao;


        public MtgCardService(IEventBusService eventBusService, ILogger<MtgCardService> logger, MtgDumpDao mtgDumpDao) : base(eventBusService, logger)
        {
            _mtgDumpDao = mtgDumpDao;
        }

        public async Task ImportFromMtg()
        {

        }

        
    }
}
