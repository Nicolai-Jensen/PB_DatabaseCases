using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class CharacterClass
{
    public short? ClassLevel { get; set; }

    public string ClassName { get; set; } = null!;

    public int CharId { get; set; }

    public virtual Character Char { get; set; } = null!;

    public virtual Class ClassNameNavigation { get; set; } = null!;
}
