using Aurora.Api.Entities.Context;
using DiceClub.Database.Entities.Account;
using DiceClub.Database.Entities.Inventory;
using Microsoft.EntityFrameworkCore;

namespace DiceClub.Database.Context
{
    public class DiceClubDbContext : BaseDbContext
    {
        public DbSet<DiceClubUser> Users { get; set; }
        public DbSet<DiceClubGroup> Groups { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryCategory> InventoryCategories { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
        public DiceClubDbContext()
        {

        }

        public DiceClubDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseSnakeCaseNamingConvention();
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseNpgsql(@"Server=127.0.0.1;Port=5432;Database=diceclub_db;User Id=postgres;Password=password;")
                    .UseSnakeCaseNamingConvention();

        }
    }
}
