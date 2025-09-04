using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEksam.ORMClasses;

public partial class Ability
{
    public string AbilityName { get; set; } = null!;

    public string AbilityType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Resource { get; set; }

    public string? DamageType { get; set; }

    public virtual ICollection<Character> Chars { get; set; } = new List<Character>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Npc> Npcs { get; set; } = new List<Npc>();
}
