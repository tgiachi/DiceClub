using Aurora.Api.Entities.Context;
using DiceClub.Database.Entities.Account;
using DiceClub.Database.Entities.Cards;
using DiceClub.Database.Entities.Cards.Deck;
using DiceClub.Database.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Mtg.Collection.Manager.Database.Entities;

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
        
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<ColorEntity> Colors { get; set; }
        public DbSet<CardTypeEntity> CardTypes { get; set; }

        public DbSet<CreatureTypeEntity> CreatureTypes { get; set; }
        public DbSet<RarityEntity> Rarities { get; set; }
        public DbSet<ColorCardEntity> CardColors { get; set; }
        public DbSet<CardSetEntity> CardSets { get; set; }
        public DbSet<CardLegalityEntity> CardLegalities { get; set; }
        public DbSet<CardLegalityTypeEntity> CardLegalityTypes { get; set; }
        public DbSet<CardCardLegality> CardCardLegalities { get; set; }

        public DbSet<DeckMasterEntity> DeckMaster { get; set; }
        public DbSet<DeckDetailEntity> DeckDetails { get; set; }
        
        public DbSet<MtgEntity> MtgDump { get; set; }

        public DiceClubDbContext()
        {

        }

        public DiceClubDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<CardEntity>()
                .HasGeneratedTsVectorColumn(p => p.SearchVector, "italian", p => new { p.CardName, p.Description, p.TypeLine })
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");
            
            model.Entity<MtgEntity>()
                .HasGeneratedTsVectorColumn(p => p.SearchVector, "italian", p => new { p.CardName, p.ForeignNames })
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");
                
            base.OnModelCreating(model);
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
