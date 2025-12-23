using System;
using System.Collections.Generic;

namespace SFWebservice.Modules;

public partial class GameSession
{
    public int SessionId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? PlayerId { get; set; }

    public string? FeedbackType { get; set; }

    public virtual ICollection<Dodge> Dodges { get; set; } = new List<Dodge>();

    public virtual ICollection<Hit> Hits { get; set; } = new List<Hit>();

    public virtual ICollection<ObjectiveCompleted> ObjectiveCompleteds { get; set; } = new List<ObjectiveCompleted>();

    public virtual Player? Player { get; set; }
}
