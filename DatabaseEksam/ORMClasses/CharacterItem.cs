using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class CharacterItem
{
    public short? Amount { get; set; }

    public int ItemId { get; set; }

    public int CharId { get; set; }

    public virtual Character Char { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
