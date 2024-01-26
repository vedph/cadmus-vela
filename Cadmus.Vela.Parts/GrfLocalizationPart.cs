using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti localization part.
/// <para>Tag: <c>it.vedph.graffiti.localization</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.localization")]
public sealed class GrfLocalizationPart : PartBase
{
    /// <summary>
    /// Gets or sets the place name.
    /// </summary>
    public ProperName? Place { get; set; }

    /// <summary>
    /// Gets or sets the period. This is a generic chronological classification
    /// (e.g. "Roman", "Medieval", "Modern", "Contemporary", etc.).
    /// </summary>
    public string? Period { get; set; }

    /// <summary>
    /// Gets or sets the support's current function (usually from
    /// <c>grf-support-functions).</c>.
    /// </summary>
    public string? Function { get; set; }

    /// <summary>
    /// Gets or sets a note about the support's original function.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Gets or sets the type of the object containing the support (usually from
    /// <c>grf-support-object-types</c>).
    /// </summary>
    public string? ObjectType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this graffiti is indoor.
    /// </summary>
    public bool Indoor { get; set; }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new(DataPinHelper.DefaultFilter);

        if (Place != null)
            builder.AddValue("place", Place.GetFullName(), filter: true);

        builder.AddValue("period", Period);
        builder.AddValue("function", Function);
        builder.AddValue("object-type", ObjectType);
        builder.AddValue("indoor", Indoor);

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
                "place",
                "The graffiti's place.",
                "f"),
            new DataPinDefinition(DataPinValueType.String,
                "period",
                "The chronological period."),
            new DataPinDefinition(DataPinValueType.String,
                "function",
                "The current function of the graffiti's support."),
            new DataPinDefinition(DataPinValueType.String,
                "object-type",
                "The graffiti's object type."),
            new DataPinDefinition(DataPinValueType.Boolean,
                "indoor",
                "True if the graffiti is indoor.")
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

        sb.Append("[GrfLocalization]:");

        if (Place != null) sb.Append(' ').Append(Place.GetFullName());
        if (Period != null) sb.Append(", ").Append(Period);
        if (Indoor) sb.Append('*');

        if (!string.IsNullOrEmpty(Function))
        {
            if (Place != null || Indoor) sb.Append(", ");
            else sb.Append(' ');
            sb.Append(Function);
        }

        return sb.ToString();
    }
}
