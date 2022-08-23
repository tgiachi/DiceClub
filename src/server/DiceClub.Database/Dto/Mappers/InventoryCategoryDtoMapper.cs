﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Aurora.Api.Entities.Impl.Dto;
using AutoMapper;
using DiceClub.Database.Entities.Inventory;

namespace DiceClub.Database.Dto.Mappers
{

    [DtoMapper(typeof(InventoryCategory), typeof(InventoryCategoryDto))]
    public class InventoryCategoryDtoMapper : AbstractDtoMapper<Guid, InventoryCategory, InventoryCategoryDto>
    {
        public InventoryCategoryDtoMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}
