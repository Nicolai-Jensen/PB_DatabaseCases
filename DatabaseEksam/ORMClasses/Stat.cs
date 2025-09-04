using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Stat
{
    public int? CharId { get; set; }

    public short Strength { get; set; }

    public short Dexterity { get; set; }

    public short Constitution { get; set; }

    public short Intelligence { get; set; }

    public short Wisdom { get; set; }

    public short Charisma { get; set; }

    public virtual Character? Char { get; set; }
}
