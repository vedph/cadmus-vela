using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti's writing part.
/// <para>Tag: <c>it.vedph.graffiti.writing</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.writing")]
public sealed class GrfWritingPart : PartBase
{
    /// <summary>
    /// Gets or sets the writing system (usually ISO 15924, lowercased,
    /// from <c>grf-writing-systems</c>).
    /// </summary>
    public string? System { get; set; }

    /// <summary>
    /// Gets or sets the text language(s) (usually ISO 639-3, from
    /// <c>grf-writing-languages</c>).
    /// </summary>
    public List<string> Languages { get; set; }

    /// <summary>
    /// Gets or sets the writing type (usually from <c>grf-writing-types</c>).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets a set of specific counts, like e.g. rows, columns,
    /// characters per line, etc. The count types and eventually tags usually
    /// depend on thesauri (<c>grf-writing-count-ids</c>,
    /// <c>grf-writing-count-tags</c>).
    /// </summary>
    public List<DecoratedCount> Counts { get; set; }

    /// <summary>
    /// Gets or sets writing features (usually from <c>grf-writing-features</c>).
    /// </summary>
    public List<string> Features { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this inscription contains
    /// some poetic text.
    /// </summary>
    public bool HasPoetry { get; set; }

    /// <summary>
    /// Gets or sets the metre(s) used in the poetic text if any (usually
    /// from <c>grf-writing-metres</c>).
    /// </summary>
    public List<string> Metres { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GrfWritingPart"/> class.
    /// </summary>
    public GrfWritingPart()
    {
        Languages = new List<string>();
        Counts = new List<DecoratedCount>();
        Features = new List<string>();
        Metres = new List<string>();
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

        builder.AddValue("system", System);
        builder.AddValues("language", Languages);
        builder.AddValue("type", Type);

        if (Counts?.Count > 0)
        {
            foreach (DecoratedCount count in Counts)
                builder.AddValue($"c-{count.Id}", count.Value);
        }

        if (Features?.Count > 0) builder.AddValues("feature", Features);

        builder.AddValue("poetic", HasPoetry);
        builder.AddValues("metre", Metres);

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
                "system",
                "The writing system."),
             new DataPinDefinition(DataPinValueType.String,
                "language",
                "The language(s) used in the text.",
                "M"),
             new DataPinDefinition(DataPinValueType.String,
                "type",
                "The writing type."),
             new DataPinDefinition(DataPinValueType.Integer,
                "c-...",
                "The counts. Each count type has its name."),
             new DataPinDefinition(DataPinValueType.String,
                "feature",
                "The writing feature(s) present."),
             new DataPinDefinition(DataPinValueType.Boolean,
                "poetic",
                "True if contains poetic text."),
             new DataPinDefinition(DataPinValueType.String,
                "metre",
                "The metre(s) used in the poetic text.",
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

        sb.Append("[EpiWriting] ").Append(System).Append(' ').Append(Type);
        if (Languages?.Count > 0)
            sb.Append(": ").AppendJoin(", ", Languages);

        return sb.ToString();
    }
}
