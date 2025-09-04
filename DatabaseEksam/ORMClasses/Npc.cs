using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Npc
{
    public int NpcId { get; set; }

    public string? NpcName { get; set; }

    public short? NpcLevel { get; set; }

    public string NpcRace { get; set; } = null!;

    public string? NpcClass { get; set; }

    public virtual Class? NpcClassNavigation { get; set; }

    public virtual Race NpcRaceNavigation { get; set; } = null!;

    public virtual ICollection<Ability> AbilityNames { get; set; } = new List<Ability>();
}
