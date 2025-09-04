using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Biome
{
    public string BiomeName { get; set; } = null!;

    public short Temperature { get; set; }

    public string? WeatherCondition { get; set; }

    public virtual ICollection<Locale> Locales { get; set; } = new List<Locale>();

    public virtual ICollection<PrimaryFauna> PrimaryFaunas { get; set; } = new List<PrimaryFauna>();
}
