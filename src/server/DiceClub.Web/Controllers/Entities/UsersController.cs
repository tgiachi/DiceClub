using Aurora.Turbine.Api.Controllers;
using Aurora.Turbine.Api.Data.Pagination;
using Aurora.Turbine.Api.Interfaces;
using DiceClub.Database.Dao;
using DiceClub.Database.Dto;
using DiceClub.Database.Dto.Mappers;
using DiceClub.Database.Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiceClub.Web.Controllers.Entities
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController : BaseRestController<Guid, DiceClubUser, DiceClubUserDto, DiceClubUserDao, DiceClubUserDtoMapper>
    {
        public UsersController(ILogger<BaseRestController<Guid, DiceClubUser, DiceClubUserDto, DiceClubUserDao, DiceClubUserDtoMapper>> logger,
            DiceClubUserDao dao,
            DiceClubUserDtoMapper mapper, IRestPaginatorService restPaginatorService) : base(logger, dao, mapper, restPaginatorService)
        {

        }

        [HttpGet]
        [Route("list")]
        public override Task<List<DiceClubUserDto>> ListAll()
        {
            return base.ListAll();
        }

        [HttpPost]
        [Route("insert")]
        public override Task<DiceClubUserDto> Insert(DiceClubUserDto dto)
        {
            return base.Insert(dto);
        }

        [HttpGet]
        [Route("list/paginate")]
        public override Task<PaginationObject<DiceClubUserDto>> ListPaginate(int page, int pageSize = 20)
        {
            return base.ListPaginate(page, pageSize);
        }

        [HttpPatch]
        [Route("update")]
        public override Task<DiceClubUserDto> Update(DiceClubUserDto dto)
        {
            return base.Update(dto);
        }

        [HttpDelete]
        [Route("delete/{id:guid}")]
        public override Task<bool> DeleteById(Guid id)
        {
            return base.DeleteById(id);
        }
    }
}
