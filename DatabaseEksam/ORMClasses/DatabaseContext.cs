using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEksam.ORMClasses;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ability> Abilities { get; set; }

    public virtual DbSet<Biome> Biomes { get; set; }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<CharacterClass> CharacterClasses { get; set; }

    public virtual DbSet<CharacterItem> CharacterItems { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Locale> Locales { get; set; }

    public virtual DbSet<Npc> Npcs { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PrimaryFauna> PrimaryFaunas { get; set; }

    public virtual DbSet<Quest> Quests { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=07N7HZpdByMgTdB2yqH8;Database=DatabaseCase2");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ability>(entity =>
        {
            entity.HasKey(e => e.AbilityName).HasName("abilities_pkey");

            entity.ToTable("abilities");

            entity.Property(e => e.AbilityName)
                .HasMaxLength(30)
                .HasColumnName("ability_name");
            entity.Property(e => e.AbilityType)
                .HasMaxLength(30)
                .HasColumnName("ability_type");
            entity.Property(e => e.DamageType)
                .HasMaxLength(30)
                .HasColumnName("damage_type");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Resource)
                .HasMaxLength(30)
                .HasColumnName("resource");

            entity.HasMany(d => d.Chars).WithMany(p => p.AbilityNames)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterAbility",
                    r => r.HasOne<Character>().WithMany()
                        .HasForeignKey("CharId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("character_abilities_char_id_fkey"),
                    l => l.HasOne<Ability>().WithMany()
                        .HasForeignKey("AbilityName")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("character_abilities_ability_name_fkey"),
                    j =>
                    {
                        j.HasKey("AbilityName", "CharId").HasName("character_abilities_pkey");
                        j.ToTable("character_abilities");
                        j.IndexerProperty<string>("AbilityName")
                            .HasMaxLength(30)
                            .HasColumnName("ability_name");
                        j.IndexerProperty<int>("CharId").HasColumnName("char_id");
                    });

            entity.HasMany(d => d.Items).WithMany(p => p.AbilityNames)
                .UsingEntity<Dictionary<string, object>>(
                    "ItemAbility",
                    r => r.HasOne<Item>().WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("item_abilities_item_id_fkey"),
                    l => l.HasOne<Ability>().WithMany()
                        .HasForeignKey("AbilityName")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("item_abilities_ability_name_fkey"),
                    j =>
                    {
                        j.HasKey("AbilityName", "ItemId").HasName("item_abilities_pkey");
                        j.ToTable("item_abilities");
                        j.IndexerProperty<string>("AbilityName")
                            .HasMaxLength(30)
                            .HasColumnName("ability_name");
                        j.IndexerProperty<int>("ItemId").HasColumnName("item_id");
                    });

            entity.HasMany(d => d.Npcs).WithMany(p => p.AbilityNames)
                .UsingEntity<Dictionary<string, object>>(
                    "NpcAbility",
                    r => r.HasOne<Npc>().WithMany()
                        .HasForeignKey("NpcId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("npc_abilities_npc_id_fkey"),
                    l => l.HasOne<Ability>().WithMany()
                        .HasForeignKey("AbilityName")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("npc_abilities_ability_name_fkey"),
                    j =>
                    {
                        j.HasKey("AbilityName", "NpcId").HasName("npc_abilities_pkey");
                        j.ToTable("npc_abilities");
                        j.IndexerProperty<string>("AbilityName")
                            .HasMaxLength(30)
                            .HasColumnName("ability_name");
                        j.IndexerProperty<int>("NpcId").HasColumnName("npc_id");
                    });
        });

        modelBuilder.Entity<Biome>(entity =>
        {
            entity.HasKey(e => e.BiomeName).HasName("biome_pkey");

            entity.ToTable("biome");

            entity.Property(e => e.BiomeName)
                .HasMaxLength(30)
                .HasColumnName("biome_name");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.WeatherCondition)
                .HasMaxLength(50)
                .HasColumnName("weather_condition");
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampaignName).HasName("campaign_pkey");

            entity.ToTable("campaign");

            entity.Property(e => e.CampaignName)
                .HasMaxLength(50)
                .HasColumnName("campaign_name");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.StartingLocale)
                .HasMaxLength(30)
                .HasColumnName("starting_locale");

            entity.HasOne(d => d.StartingLocaleNavigation).WithMany(p => p.Campaigns)
                .HasForeignKey(d => d.StartingLocale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("campaign_starting_locale_fkey");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharId).HasName("characters_pkey");

            entity.ToTable("characters");

            entity.Property(e => e.CharId).HasColumnName("char_id");
            entity.Property(e => e.CampaignName)
                .HasMaxLength(50)
                .HasColumnName("campaign_name");
            entity.Property(e => e.CharLevel)
                .HasDefaultValue((short)1)
                .HasColumnName("char_level");
            entity.Property(e => e.CharName)
                .HasMaxLength(30)
                .HasColumnName("char_name");
            entity.Property(e => e.CharRace)
                .HasMaxLength(30)
                .HasColumnName("char_race");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Feats)
                .HasMaxLength(100)
                .HasColumnName("feats");
            entity.Property(e => e.Provenance)
                .HasMaxLength(30)
                .HasColumnName("provenance");
            entity.Property(e => e.XPosition).HasColumnName("x_position");
            entity.Property(e => e.YPosition).HasColumnName("y_position");

            entity.HasOne(d => d.CampaignNameNavigation).WithMany(p => p.Characters)
                .HasForeignKey(d => d.CampaignName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("characters_campaign_name_fkey");

            entity.HasOne(d => d.CharRaceNavigation).WithMany(p => p.Characters)
                .HasForeignKey(d => d.CharRace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("characters_char_race_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Characters)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("characters_created_by_fkey");

            entity.HasOne(d => d.ProvenanceNavigation).WithMany(p => p.Characters)
                .HasForeignKey(d => d.Provenance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("characters_provenance_fkey");
        });

        modelBuilder.Entity<CharacterClass>(entity =>
        {
            entity.HasKey(e => new { e.ClassName, e.CharId }).HasName("character_classes_pkey");

            entity.ToTable("character_classes");

            entity.Property(e => e.ClassName)
                .HasMaxLength(30)
                .HasColumnName("class_name");
            entity.Property(e => e.CharId).HasColumnName("char_id");
            entity.Property(e => e.ClassLevel).HasColumnName("class_level");

            entity.HasOne(d => d.Char).WithMany(p => p.CharacterClasses)
                .HasForeignKey(d => d.CharId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_classes_char_id_fkey");

            entity.HasOne(d => d.ClassNameNavigation).WithMany(p => p.CharacterClasses)
                .HasForeignKey(d => d.ClassName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_classes_class_name_fkey");
        });

        modelBuilder.Entity<CharacterItem>(entity =>
        {
            entity.HasKey(e => new { e.ItemId, e.CharId }).HasName("character_items_pkey");

            entity.ToTable("character_items");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.CharId).HasColumnName("char_id");
            entity.Property(e => e.Amount)
                .HasDefaultValue((short)1)
                .HasColumnName("amount");

            entity.HasOne(d => d.Char).WithMany(p => p.CharacterItems)
                .HasForeignKey(d => d.CharId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_items_char_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.CharacterItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_items_item_id_fkey");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassName).HasName("class_pkey");

            entity.ToTable("class");

            entity.Property(e => e.ClassName)
                .HasMaxLength(30)
                .HasColumnName("class_name");
            entity.Property(e => e.CastingModifier)
                .HasMaxLength(30)
                .HasColumnName("casting_modifier");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => new { e.SlotType, e.CharId }).HasName("equipment_pkey");

            entity.ToTable("equipment");

            entity.Property(e => e.SlotType)
                .HasMaxLength(30)
                .HasColumnName("slot_type");
            entity.Property(e => e.CharId).HasColumnName("char_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");

            entity.HasOne(d => d.Char).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.CharId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipment_char_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("equipment_item_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("items_pkey");

            entity.ToTable("items");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemLevel).HasColumnName("item_level");
            entity.Property(e => e.ItemName)
                .HasMaxLength(30)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemType)
                .HasMaxLength(30)
                .HasColumnName("item_type");
        });

        modelBuilder.Entity<Locale>(entity =>
        {
            entity.HasKey(e => e.LocaleName).HasName("locale_pkey");

            entity.ToTable("locale");

            entity.Property(e => e.LocaleName)
                .HasMaxLength(30)
                .HasColumnName("locale_name");
            entity.Property(e => e.BiomeName)
                .HasMaxLength(30)
                .HasColumnName("biome_name");
            entity.Property(e => e.LocaleType)
                .HasMaxLength(30)
                .HasColumnName("locale_type");

            entity.HasOne(d => d.BiomeNameNavigation).WithMany(p => p.Locales)
                .HasForeignKey(d => d.BiomeName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("locale_biome_name_fkey");
        });

        modelBuilder.Entity<Npc>(entity =>
        {
            entity.HasKey(e => e.NpcId).HasName("npc_pkey");

            entity.ToTable("npc");

            entity.Property(e => e.NpcId).HasColumnName("npc_id");
            entity.Property(e => e.NpcClass)
                .HasMaxLength(30)
                .HasColumnName("npc_class");
            entity.Property(e => e.NpcLevel).HasColumnName("npc_level");
            entity.Property(e => e.NpcName)
                .HasMaxLength(30)
                .HasColumnName("npc_name");
            entity.Property(e => e.NpcRace)
                .HasMaxLength(30)
                .HasColumnName("npc_race");

            entity.HasOne(d => d.NpcClassNavigation).WithMany(p => p.Npcs)
                .HasForeignKey(d => d.NpcClass)
                .HasConstraintName("npc_npc_class_fkey");

            entity.HasOne(d => d.NpcRaceNavigation).WithMany(p => p.Npcs)
                .HasForeignKey(d => d.NpcRace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("npc_npc_race_fkey");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("players_pkey");

            entity.ToTable("players");

            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");
        });

        modelBuilder.Entity<PrimaryFauna>(entity =>
        {
            entity.HasKey(e => new { e.BiomeName, e.FaunaName }).HasName("primary_fauna_pkey");

            entity.ToTable("primary_fauna");

            entity.Property(e => e.BiomeName)
                .HasMaxLength(30)
                .HasColumnName("biome_name");
            entity.Property(e => e.FaunaName)
                .HasMaxLength(30)
                .HasColumnName("fauna_name");

            entity.HasOne(d => d.BiomeNameNavigation).WithMany(p => p.PrimaryFaunas)
                .HasForeignKey(d => d.BiomeName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("primary_fauna_biome_name_fkey");
        });

        modelBuilder.Entity<Quest>(entity =>
        {
            entity.HasKey(e => e.QuestId).HasName("quests_pkey");

            entity.ToTable("quests");

            entity.Property(e => e.QuestId).HasColumnName("quest_id");
            entity.Property(e => e.GoldValue)
                .HasDefaultValue((short)1)
                .HasColumnName("gold_value");
            entity.Property(e => e.Xp)
                .HasDefaultValue(1)
                .HasColumnName("xp");

            entity.HasMany(d => d.CampaignNames).WithMany(p => p.Quests)
                .UsingEntity<Dictionary<string, object>>(
                    "QuestsInCampaign",
                    r => r.HasOne<Campaign>().WithMany()
                        .HasForeignKey("CampaignName")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("quests_in_campaign_campaign_name_fkey"),
                    l => l.HasOne<Quest>().WithMany()
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("quests_in_campaign_quest_id_fkey"),
                    j =>
                    {
                        j.HasKey("QuestId", "CampaignName").HasName("quests_in_campaign_pkey");
                        j.ToTable("quests_in_campaign");
                        j.IndexerProperty<int>("QuestId").HasColumnName("quest_id");
                        j.IndexerProperty<string>("CampaignName")
                            .HasMaxLength(50)
                            .HasColumnName("campaign_name");
                    });

            entity.HasMany(d => d.Chars).WithMany(p => p.Quests)
                .UsingEntity<Dictionary<string, object>>(
                    "CompletedQuest",
                    r => r.HasOne<Character>().WithMany()
                        .HasForeignKey("CharId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("completed_quests_char_id_fkey"),
                    l => l.HasOne<Quest>().WithMany()
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("completed_quests_quest_id_fkey"),
                    j =>
                    {
                        j.HasKey("QuestId", "CharId").HasName("completed_quests_pkey");
                        j.ToTable("completed_quests");
                        j.IndexerProperty<int>("QuestId").HasColumnName("quest_id");
                        j.IndexerProperty<int>("CharId").HasColumnName("char_id");
                    });
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.RaceName).HasName("race_pkey");

            entity.ToTable("race");

            entity.Property(e => e.RaceName)
                .HasMaxLength(30)
                .HasColumnName("race_name");
            entity.Property(e => e.Skill)
                .HasMaxLength(30)
                .HasColumnName("skill");
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("stats");

            entity.Property(e => e.CharId).HasColumnName("char_id");
            entity.Property(e => e.Charisma).HasColumnName("charisma");
            entity.Property(e => e.Constitution).HasColumnName("constitution");
            entity.Property(e => e.Dexterity).HasColumnName("dexterity");
            entity.Property(e => e.Intelligence).HasColumnName("intelligence");
            entity.Property(e => e.Strength).HasColumnName("strength");
            entity.Property(e => e.Wisdom).HasColumnName("wisdom");

            entity.HasOne(d => d.Char).WithMany()
                .HasForeignKey(d => d.CharId)
                .HasConstraintName("stats_char_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
