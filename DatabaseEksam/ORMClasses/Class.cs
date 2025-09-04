using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Class
{
    public string ClassName { get; set; } = null!;

    public string CastingModifier { get; set; } = null!;

    public virtual ICollection<CharacterClass> CharacterClasses { get; set; } = new List<CharacterClass>();

    public virtual ICollection<Npc> Npcs { get; set; } = new List<Npc>();
}
