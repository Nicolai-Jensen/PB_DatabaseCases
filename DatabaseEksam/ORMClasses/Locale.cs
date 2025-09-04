using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Locale
{
    public string LocaleName { get; set; } = null!;

    public string LocaleType { get; set; } = null!;

    public string BiomeName { get; set; } = null!;

    public virtual Biome BiomeNameNavigation { get; set; } = null!;

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
