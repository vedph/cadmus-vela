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
    /// Gets or sets the frame type (usually from <c>grf-fig-frame-types</c>.
    /// </summary>
    public string? FrameType { get; set; }

    /// <summary>
    /// Gets or sets the figurative type (usually from a hierarchic
    /// <c>grf-fig-types</c>).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the main figurative features (usually from
    /// <c>grf-fig-features</c>).
    /// </summary>
    public List<string> Features { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GrfFigurativePart"/> class.
    /// </summary>
    public GrfFigurativePart()
    {
        Features = new List<string>();
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

        builder.AddValue("frame-type", FrameType);
        builder.AddValue("type", Type);
        if (Features?.Count > 0) builder.AddValues("feature", Features);

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
                "frame-type",
                "The frame type."),
             new DataPinDefinition(DataPinValueType.String,
                "type",
                "The main figurative type."),
             new DataPinDefinition(DataPinValueType.String,
                "feature",
                "The main figurative feature(s).",
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

        sb.Append("[GrfFigurative]");

        if (!string.IsNullOrEmpty(FrameType))
            sb.Append('[').Append(FrameType).Append(']');

        if (!string.IsNullOrEmpty(Type))
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(Type);
        }

        if (Features?.Count > 0)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append('(').Append(Features.Count).Append(')');
        }

        return sb.ToString();
    }
}
