using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemType { get; set; } = null!;

    public short? ItemLevel { get; set; }

    public virtual ICollection<CharacterItem> CharacterItems { get; set; } = new List<CharacterItem>();

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<Ability> AbilityNames { get; set; } = new List<Ability>();
}
