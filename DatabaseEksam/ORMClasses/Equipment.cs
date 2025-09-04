using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Equipment
{
    public string SlotType { get; set; } = null!;

    public int CharId { get; set; }

    public int? ItemId { get; set; }

    public virtual Character Char { get; set; } = null!;

    public virtual Item? Item { get; set; }
}
