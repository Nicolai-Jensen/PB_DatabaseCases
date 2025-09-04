using System;
using System.Collections.Generic;

namespace DatabaseEksam.ORMClasses;

public partial class Player
{
    public int PlayerId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
