using System;

namespace Cadmus.Vela.Parts;

/// <summary>
/// A recorded state for the graffiti.
/// </summary>
public class GrfSupportState
{
    /// <summary>
    /// Gets or sets the state type (usually from <c>grf-support-states</c>).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the date of this state report.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the name of who reported this state.
    /// </summary>
    public string? Reporter { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"{Type} on {Date} by {Reporter}";
    }
}
