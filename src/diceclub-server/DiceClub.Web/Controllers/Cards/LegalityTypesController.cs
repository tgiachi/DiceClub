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

[Route("api/v1/cards/legality_type")]
public class LegalityTypesController :  BaseCrudAuthController<MtgCardLegalityTypeEntity, MtgCardLegalityTypeDto, DiceClubDbContext>
{
    public LegalityTypesController(DiceClubUserDao diceClubUserDao, ILogger<LegalityTypesController> logger, MtgCardLegalityTypeDao dataAccess, MtgCardLegalityTypeMapper dtoMapper, DiceClubDbContext dbContext, RestPaginatorService restPaginatorService) : base(diceClubUserDao, logger, dataAccess, dtoMapper, dbContext, restPaginatorService)
    {
        CanDelete = false;
    }
}