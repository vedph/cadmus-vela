using Cadmus.Core;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti material support part.
/// <para>Tag: <c>it.vedph.graffiti.support</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.support")]
public sealed class GrfSupportPart : PartBase
{
    /// <summary>
    /// Gets or sets the type of the support (usually from <c>grf-support-types</c>).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the material (usually from <c>grf-support-materials</c>).
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets an optional note.
    /// </summary>
    public string? Note { get; set; }

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

        builder.AddValue("type", Type);
        builder.AddValue("material", Material);

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
                "The graffiti's support type."),
            new DataPinDefinition(DataPinValueType.String,
                "material",
                "The material of the graffiti's support."),
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

        sb.Append("[GrfSupport]");

        if (!string.IsNullOrEmpty(Type)) sb.Append(' ').Append(Type);

        if (!string.IsNullOrEmpty(Material))
            sb.Append(" (").Append(Material).Append(')');

        return sb.ToString();
    }
}
