using System;
using System.Collections.Generic;

namespace SFWebservice.Modules;

public partial class Player
{
    public int PlayerId { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();

    public virtual ICollection<Hit> Hits { get; set; } = new List<Hit>();
}
