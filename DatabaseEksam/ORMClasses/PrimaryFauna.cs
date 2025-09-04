using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class PrimaryFauna
{
    public string BiomeName { get; set; } = null!;

    public string FaunaName { get; set; } = null!;

    public virtual Biome BiomeNameNavigation { get; set; } = null!;
}
