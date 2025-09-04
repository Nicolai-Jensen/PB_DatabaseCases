using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Race
{
    public string RaceName { get; set; } = null!;

    public string? Skill { get; set; }

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual ICollection<Npc> Npcs { get; set; } = new List<Npc>();
}
