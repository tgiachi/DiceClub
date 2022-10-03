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
public class TypesController : BaseCrudAuthController<MtgCardTypeEntity, MtgCardTypeDto, DiceClubDbContext>
{
    public TypesController(DiceClubUserDao diceClubUserDao, ILogger<TypesController> logger, MtgCardTypeDao dataAccess, MtgCardTypeMapper dtoMapper, DiceClubDbContext dbContext, RestPaginatorService restPaginatorService) : base(diceClubUserDao, logger, dataAccess, dtoMapper, dbContext, restPaginatorService)
    {
        CanDelete = false;
    }
}