using Cadmus.Core;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti states part.
/// <para>Tag: <c>it.vedph.graffiti.states</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.states")]
public sealed class GrfStatesPart : PartBase
{
    /// <summary>
    /// Gets or sets the entries.
    /// </summary>
    public List<GrfState> States { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GrfStatePart"/> class.
    /// </summary>
    public GrfStatesPart()
    {
        States = new List<GrfState>();
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: <c>type</c>=type.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new(new StandardDataPinTextFilter());

        builder.Set("tot", States?.Count ?? 0, false);

        if (States?.Count > 0)
        {
            foreach (GrfState state in States)
                builder.AddValue("type", state.Type);
        }

        return builder.Build(this);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public override IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return new List<DataPinDefinition>(new[]
        {
            new DataPinDefinition(DataPinValueType.Integer,
               "tot-count",
               "The total count of states."),
            new DataPinDefinition(DataPinValueType.Integer,
               "state",
               "The graffiti's states.",
               "M")
        });
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append("[GrfStates]");

        if (States?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in States)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (States.Count > 3)
                sb.Append("...(").Append(States.Count).Append(')');
        }

        return sb.ToString();
    }
}
