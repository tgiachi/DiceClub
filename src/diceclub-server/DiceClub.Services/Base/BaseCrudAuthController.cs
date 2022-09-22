using Aurora.Api.Entities.Context;
using Aurora.Api.Entities.Impl.Dao;
using Aurora.Api.Entities.Impl.Dto;
using Aurora.Api.Entities.Impl.Entities;
using DiceClub.Api.Data.Rest;
using DiceClub.Database.Dao.Account;
using DiceClub.Services.Paginator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Base;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseCrudAuthController<TEntity, TDto, TDbContext> : BaseGroupAuthController
    where TEntity : BaseGuidEntity
    where TDto : AbstractGuidDtoEntity
    where TDbContext : BaseDbContext
{
    private readonly AbstractDataAccess<Guid, TEntity, TDbContext> _dataAccess;
    private readonly AbstractDtoMapper<Guid, TEntity, TDto> _dtoMapper;
    private readonly TDbContext _dbContext;
    private readonly RestPaginatorService _restPaginatorService;

    public bool CanDelete { get; set; } = true;

    public BaseCrudAuthController(DiceClubUserDao diceClubUserDao,
        ILogger<BaseCrudAuthController<TEntity, TDto, TDbContext>> logger,
        AbstractDataAccess<Guid, TEntity, TDbContext> dataAccess,
        AbstractDtoMapper<Guid, TEntity, TDto> dtoMapper,
        TDbContext dbContext, RestPaginatorService restPaginatorService) : base(diceClubUserDao, logger)
    {
        _dataAccess = dataAccess;
        _dtoMapper = dtoMapper;
        _dbContext = dbContext;
        _restPaginatorService = restPaginatorService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedRestResultObject<List<TDto>>>> FindAll(int page = 1, int pageSize = 30)
    {
        var useRoute = await UserCanUseRoute();
        if (!useRoute)
        {
            return DoUnauthorizedPaged<List<TDto>>();
        }

        return Ok(await _restPaginatorService.Paginate<Guid, TEntity, TDto, AbstractDtoMapper<Guid, TEntity, TDto>>(
            await _dataAccess.FindAll(), page, pageSize, _dtoMapper));
    }

    [HttpPost]
    public async Task<ActionResult<RestResultObject<TDto>>> Insert([FromBody] TDto dto)
    {
        var useRoute = await UserCanUseRoute();
        if (!useRoute)
        {
            return DoUnauthorizedObject<TDto>();
        }

        try
        {
            var dtoEntity = _dtoMapper.ToDto(await _dataAccess.Insert(_dtoMapper.ToEntity(dto)));
            return Ok(RestResultObjectBuilder<TDto>.Create().Data(dtoEntity).Build());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                RestResultObjectBuilder<TDto>.Create().Error(ex).Build());
        }
        
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<RestResultObject<TDto>>> FindById(Guid id)
    {
        var useRoute = await UserCanUseRoute();
        if (!useRoute)
        {
            return DoUnauthorizedObject<TDto>();
        }

        try
        {
            var dtoEntity = _dtoMapper.ToDto(await _dataAccess.FindById(id));
            return Ok(RestResultObjectBuilder<TDto>.Create().Data(dtoEntity).Build());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                RestResultObjectBuilder<TDto>.Create().Error(ex).Build());
        }
        
    }
    
    [HttpPatch]
    public async Task<ActionResult<RestResultObject<TDto>>> Update([FromBody] TDto dto)
    {
        var useRoute = await UserCanUseRoute();
        if (!useRoute)
        {
            return DoUnauthorizedObject<TDto>();
        }

        try
        {
            var dtoEntity = _dtoMapper.ToDto(await _dataAccess.Update(_dtoMapper.ToEntity(dto)));
            return Ok(RestResultObjectBuilder<TDto>.Create().Data(dtoEntity).Build());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                RestResultObjectBuilder<TDto>.Create().Error(ex).Build());
        }
        
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ActionResult<RestResultObject<bool>>> Delete(Guid id)
    {
        if (!CanDelete)
        {
            return DoUnauthorizedObject<bool>();
        }
        
        var useRoute = await UserCanUseRoute();
        if (!useRoute)
        {
            return DoUnauthorizedObject<bool>();
        }

        try
        {
            var deleteResult = await _dataAccess.Delete(id);
            return Ok(RestResultObjectBuilder<bool>.Create().Data(deleteResult).Build());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                RestResultObjectBuilder<TDto>.Create().Error(ex).Build());
        }
    }

    private ActionResult<PaginatedRestResultObject<TData>> DoUnauthorizedPaged<TData>()
    {
        return Unauthorized(PaginatedRestResultObjectBuilder<TData>.Create().Error(new Exception("Unauthorized"))
            .Build());
    }

    private ActionResult<RestResultObject<TData>> DoUnauthorizedObject<TData>()
    {
        return Unauthorized(RestResultObjectBuilder<TData>.Create().Error(new Exception("Unauthorized"))
            .Build());
    }
}