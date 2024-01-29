using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti's figurative description part.
/// <para>Tag: <c>it.vedph.graffiti.figurative</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.figurative")]
public sealed class GrfFigurativePart : PartBase
{
    /// <summary>
    /// Gets or sets the figurative type (usually from a hierarchic
    /// <c>grf-figurative-types</c>).
    /// </summary>
    public List<string> Types { get; set; }

    /// <summary>
    /// Gets or sets the figurative description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GrfFigurativePart"/> class.
    /// </summary>
    public GrfFigurativePart()
    {
        Types = [];
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

        builder.AddValues("type", Types);

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
                "type",
                "The figurative type(s).",
                "M"),
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

        sb.Append("[GrfFigurative]");

        if (Types?.Count > 0)
        {
            sb.Append(' ').AppendJoin(", ", Types);
        }

        return sb.ToString();
    }
}
