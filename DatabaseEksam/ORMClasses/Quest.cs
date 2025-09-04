using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Quest
{
    public int QuestId { get; set; }

    public short? GoldValue { get; set; }

    public int? Xp { get; set; }

    public virtual ICollection<Campaign> CampaignNames { get; set; } = new List<Campaign>();

    public virtual ICollection<Character> Chars { get; set; } = new List<Character>();
}
