using System;
using System.Collections.Generic;

namespace SFWebservice.Modules;

public partial class Hit
{
    public int HitId { get; set; }

    public bool EntityHitId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public int? GameSessionId { get; set; }

    public virtual GameSession? GameSession { get; set; }
}
