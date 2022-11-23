using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Attributes;
using Aurora.Api.Utils;
using Autofac;
using DiceClub.Api.Attributes.Inventory;

namespace DiceClub.Services.Modules
{

    [ModuleLoader]
    public class CategoryParserLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var attributes =  AssemblyUtils.GetAttribute<InventoryHandlerAttribute>();

            foreach (var attribute in attributes)
            {
                builder.RegisterType(attribute).AsSelf();
            }
            base.Load(builder);
        }
    }
}
