using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Entities.Context;
using DiceClub.Database.Entities.Account;
using DiceClub.Database.Entities.Deck;
using DiceClub.Database.Entities.MtgCards;
using Microsoft.EntityFrameworkCore;

namespace DiceClub.Database.Context
{
    public class DiceClubDbContext : BaseDbContext
    {
        #region Accounting

        public DbSet<DiceClubUser> Users { get; set; }
        public DbSet<DiceClubGroup> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        #endregion

        #region MtgCards

        public DbSet<MtgCardEntity> MtgCards { get; set; }
        public DbSet<MtgCardColorEntity> MtgCardColors { get; set; }
        public DbSet<MtgCardColorRelEntity> MtgCardsColorsRel { get; set; }
        public DbSet<MtgCardLanguageEntity> MtgCardLanguages { get; set; }
        public DbSet<MtgCardLegalityEntity> MtgCardLegalities { get; set; }
        public DbSet<MtgCardLegalityRelEntity> MtgCardsLegalities { get; set; }
        public DbSet<MtgCardLegalityTypeEntity> MtgCardLegalityTypes { get; set; }
        public DbSet<MtgCardRarityEntity> MtgCardRarities { get; set; }
        public DbSet<MtgCardSetEntity> MtgCardSets { get; set; }
        public DbSet<MtgCardTypeEntity> MtgCardTypes { get; set; }
        public DbSet<MtgDumpEntity> MtgDumpEntities { get; set; }
        public DbSet<MtgCardStageEntity> CardsStage { get; set; }
        public DbSet<MtgCardSymbolEntity> MtgCardSymbols { get; set; }

        #endregion

        public DbSet<DeckMasterEntity> DeckMaster { get; set; }
        public DbSet<DeckDetailEntity> DeckDetails { get; set; }


        public DiceClubDbContext()
        {
        }

        public DiceClubDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<MtgCardEntity>()
                .HasGeneratedTsVectorColumn(p => p.SearchVector, "italian",
                    p => new { p.Name, p.Description, p.TypeLine, p.PrintedName, p.ForeignNames })
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");


            model.Entity<MtgDumpEntity>()
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
                    .UseNpgsql(
                        @"Server=127.0.0.1;Port=5432;Database=diceclub_prod_db;User Id=postgres;Password=password;")
                    .UseSnakeCaseNamingConvention();
        }
    }
}