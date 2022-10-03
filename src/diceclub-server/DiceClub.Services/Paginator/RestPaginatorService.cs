using Aurora.Api.Entities.Interfaces.Dto;
using Aurora.Api.Entities.Interfaces.Entities;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Rest;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services.Paginator
{
    public class RestPaginatorService : AbstractBaseService<RestPaginatorService>
    {
        public RestPaginatorService(IEventBusService eventBusService, ILogger<RestPaginatorService> logger) : base(
            eventBusService, logger)
        {
        }

        public Task<PaginatedRestResultObject<TEntity>> Paginate<TId, TEntity>(IQueryable<TEntity> resultQueryObjects,
            int page, int pageSize) where TEntity : IBaseEntity<TId>
        {
            return Paginate<TId, TEntity>(resultQueryObjects.ToList(), page, pageSize);
        }

        public Task<PaginatedRestResultObject<TEntity>> Paginate<TId, TEntity>(List<TEntity> resultQueryObjects,
            int page,
            int pageSize) where TEntity : IBaseEntity<TId>
        {
            var totalCount = resultQueryObjects.Count();
            page = page < 1 ? 1 : page;

            
            resultQueryObjects = resultQueryObjects.Skip((page - 1) * pageSize).Take(pageSize).ToList();


            return Task.FromResult(PaginatedRestResultObjectBuilder<TEntity>
                .Create()
                .PageSize(pageSize)
                .Total(totalCount)
                .Page(page)
                .PageCount((int)Math.Ceiling((double)totalCount / pageSize))
                .Data(resultQueryObjects)
                .Build());
        }

        public async Task<PaginatedRestResultObject<TDto>> Paginate<TId, TEntity, TDto, TDtoMapper>(
            List<TEntity> resultQueryObjects,
            int page,
            int pageSize,
            TDtoMapper mapper,
            long totalCount = 0
        ) where TEntity : IBaseEntity<TId>
            where TDto : IBaseDto<TId>
            where TDtoMapper : IDtoMapper<TId, TEntity, TDto>
        {
            var result = await Paginate<TId, TEntity>(resultQueryObjects, page, pageSize);
            if (totalCount > 0)
            {
                result.Count = totalCount;
            }
            return await PaginateToDto<TId, TEntity, TDto, TDtoMapper>(result, mapper);
        }

        public Task<PaginatedRestResultObject<TDto>> Paginate<TId, TEntity, TDto, TDtoMapper>(
            IQueryable<TEntity> resultQueryObjects,
            int page,
            int pageSize,
            TDtoMapper mapper) where TEntity : IBaseEntity<TId>
            where TDto : IBaseDto<TId>
            where TDtoMapper : IDtoMapper<TId, TEntity, TDto>
        {
            return Paginate<TId, TEntity, TDto, TDtoMapper>(resultQueryObjects.ToList(), page, pageSize, mapper);
        }


        public Task<PaginatedRestResultObject<TDto>> PaginateToDto<TId, TEntity, TDto, TDtoMapper>(
            PaginatedRestResultObject<TEntity> paginationObject, TDtoMapper mapper)
            where TEntity : IBaseEntity<TId>
            where TDto : IBaseDto<TId>
            where TDtoMapper : IDtoMapper<TId, TEntity, TDto>
        {
            return Task.FromResult(new PaginatedRestResultObject<TDto>()
            {
                PageCount = paginationObject.PageCount,
                PageSize = paginationObject.PageSize,
                Count = paginationObject.Count,
                Result = mapper.ToDto(paginationObject.Result),
                Page = paginationObject.Page
            });
        }
    }
}