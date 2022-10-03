using Aurora.Api.Entities.Impl.Dao;
using Aurora.Api.Entities.Impl.Dto;
using DiceClub.Database.Context;
using DiceClub.Database.Dao.Account;
using DiceClub.Database.Dao.Cards;
using DiceClub.Database.Dto.Cards;
using DiceClub.Database.Dto.Mappers.Cards;
using DiceClub.Database.Entities.MtgCards;
using DiceClub.Services.Base;
using DiceClub.Services.Paginator;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Cards;

[Route("api/v1/cards/[controller]")]
public class ColorsController : BaseCrudAuthController<MtgCardColorEntity, MtgCardColorDto, DiceClubDbContext>
{
    public ColorsController(DiceClubUserDao diceClubUserDao, ILogger<ColorsController> logger, MtgCardColorDao dataAccess,MtgCardColorMapper dtoMapper, DiceClubDbContext dbContext, RestPaginatorService restPaginatorService) : base(diceClubUserDao, logger, dataAccess, dtoMapper, dbContext, restPaginatorService)
    {
        CanDelete = false;
    }
}