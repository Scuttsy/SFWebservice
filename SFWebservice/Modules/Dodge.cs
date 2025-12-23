using System;
using System.Collections.Generic;

namespace SFWebservice.Modules;

public partial class Dodge
{
    public int DodgeId { get; set; }

    public int? GameSessionId { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual GameSession? GameSession { get; set; }
}
