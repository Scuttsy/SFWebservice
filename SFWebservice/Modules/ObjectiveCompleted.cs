using System;
using System.Collections.Generic;

namespace SFWebservice.Modules;

public partial class ObjectiveCompleted
{
    public int ObjectiveCompletedId { get; set; }

    public int? GameSessionId { get; set; }

    public int? ObjectiveId { get; set; }

    public DateTime? TimeCompleted { get; set; }

    public virtual Objective? Objective { get; set; }

    public virtual GameSession? GameSession { get; set; }
}
