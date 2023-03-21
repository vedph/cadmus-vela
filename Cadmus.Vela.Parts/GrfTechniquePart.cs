using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti's technique description part.
/// <para>Tag: <c>it.vedph.graffiti.technique</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.technique")]
public sealed class GrfTechniquePart : PartBase
{
    /// <summary>
    /// Gets or sets the technique(s) used in the graffiti. Usually from
    /// <c>grf-techniques</c>.
    /// </summary>
    public List<string> Techniques { get; set; }

    /// <summary>
    /// Gets or sets the tool(s) used to make the graffiti. Usually from
    /// <c>grf-tools</c>.
    /// </summary>
    public List<string> Tools { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GrfTechniquePart"/> class.
    /// </summary>
    public GrfTechniquePart()
    {
        Techniques = new List<string>();
        Tools = new List<string>();
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        if (Techniques?.Count > 0) builder.AddValues("technique", Techniques);
        if (Tools?.Count > 0) builder.AddValues("tool", Tools);

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
             new DataPinDefinition(DataPinValueType.String,
                "technique",
                "The graffiti's techniques.",
                "M"),
             new DataPinDefinition(DataPinValueType.String,
                "tool",
                "The graffiti's tools.",
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

        sb.Append("[GrfTechnique]");

        if (Techniques?.Count > 0) sb.AppendJoin(", ", Techniques);
        if (Tools?.Count > 0)
        {
            if (sb.Length > 0) sb.Append(" - ");
            sb.AppendJoin(", ", Tools);
        }

        return sb.ToString();
    }
}
