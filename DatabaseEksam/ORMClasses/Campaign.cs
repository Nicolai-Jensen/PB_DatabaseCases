using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Campaign
{
    public string CampaignName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string StartingLocale { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual Locale StartingLocaleNavigation { get; set; } = null!;

    public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();
}
