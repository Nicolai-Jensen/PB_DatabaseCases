using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Character
{
    public int CharId { get; set; }

    public string CharName { get; set; } = null!;

    public short? CharLevel { get; set; }

    public string? Feats { get; set; }

    public int XPosition { get; set; }

    public int YPosition { get; set; }

    public string Provenance { get; set; } = null!;

    public string CharRace { get; set; } = null!;

    public string CampaignName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public virtual Campaign CampaignNameNavigation { get; set; } = null!;

    public virtual Race CharRaceNavigation { get; set; } = null!;

    public virtual ICollection<CharacterClass> CharacterClasses { get; set; } = new List<CharacterClass>();

    public virtual ICollection<CharacterItem> CharacterItems { get; set; } = new List<CharacterItem>();

    public virtual Player CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual Locale ProvenanceNavigation { get; set; } = null!;

    public virtual ICollection<Ability> AbilityNames { get; set; } = new List<Ability>();

    public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();
}
