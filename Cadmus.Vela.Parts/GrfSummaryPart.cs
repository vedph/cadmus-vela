using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Cadmus.Mat.Bricks;
using Cadmus.Refs.Bricks;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Configuration;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti summary part.
/// <para>Tag: <c>it.vedph.graffiti.summary</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.summary")]
public sealed class GrfSummaryPart : PartBase
{
    /// <summary>
    /// Gets or sets the place name.
    /// </summary>
    public ProperName? Place { get; set; }

    /// <summary>
    /// Gets or sets the type of the support (usually from
    /// <c>grf-support-types</c>).
    /// </summary>
    public string? SupportType { get; set; }

    /// <summary>
    /// Gets or sets the type of the object containing the support (usually from
    /// <c>grf-support-object-types</c>).
    /// </summary>
    public string? ObjectType { get; set; }

    /// <summary>
    /// Gets or sets the support's original function (usually from
    /// <c>grf-support-functions).</c>.
    /// </summary>
    public string? OriginalFn { get; set; }

    /// <summary>
    /// Gets or sets the support's current function (usually from
    /// <c>grf-support-functions).</c>.
    /// </summary>
    public string? CurrentFn { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this graffiti is indoor.
    /// </summary>
    public bool Indoor { get; set; }

    /// <summary>
    /// Gets or sets the material (usually from <c>grf-support-materials</c>).
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the material description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the graffiti's size.
    /// </summary>
    public PhysicalSize? Size { get; set; }

    /// <summary>
    /// Gets or sets the graffiti's date.
    /// </summary>
    public HistoricalDate? Date { get; set; }

    /// <summary>
    /// Gets or sets the graffiti's main features (usually from
    /// <c>grf-features</c>).
    /// </summary>
    public List<string> Features { get; set; }

    /// <summary>
    /// Gets or sets the figurative description.
    /// </summary>
    public string? FigDescription { get; set; }

    /// <summary>
    /// Gets or sets the frame description.
    /// </summary>
    public string? FrameDescription { get; set; }

    /// <summary>
    /// Gets or sets the last seen date and time (usually UTC).
    /// </summary>
    public DateTime LastSeen { get; set; }

    /// <summary>
    /// Gets or sets the recorded state(s) for this graffiti.
    /// </summary>
    public List<GrfSupportState> States { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GrfSummaryPart"/> class.
    /// </summary>
    public GrfSummaryPart()
    {
        Features = new List<string>();
        LastSeen = DateTime.UtcNow;
        States = new List<GrfSupportState>();
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
        DataPinBuilder builder = new(DataPinHelper.DefaultFilter);

        if (Place != null)
        {
            builder.AddValue("place", Place.GetFullName(), filter: true);
        }

        builder.AddValue("support-type", SupportType);
        builder.AddValue("object-type", ObjectType);
        builder.AddValue("original-fn", OriginalFn);
        builder.AddValue("current-fn", CurrentFn);
        builder.AddValue("indoor", Indoor);
        builder.AddValue("material", Material);
        if (Size != null)
        {
            if (Size.W != null) builder.AddValue("w", Size.W.Value);
            if (Size.H != null) builder.AddValue("h", Size.H.Value);
        }
        if (Date is not null)
        {
            builder.AddValue("date-value",
                Date.GetSortValue().ToString(CultureInfo.InvariantCulture));
        }
        if (Features?.Count > 0) builder.AddValues("feature", Features);

        builder.AddValue("yr-last-seen",
            LastSeen.Year.ToString(CultureInfo.InvariantCulture));

        if (States?.Count > 0)
        {
            builder.AddValues("state", States.Select(s => s.Type!));
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
            new DataPinDefinition(DataPinValueType.String,
                "place",
                "The graffiti's place.",
                "f"),
            new DataPinDefinition(DataPinValueType.String,
                "support-type",
                "The graffiti's support type."),
            new DataPinDefinition(DataPinValueType.String,
                "object-type",
                "The graffiti's object type."),
            new DataPinDefinition(DataPinValueType.String,
                "original-fn",
                "The original function of the graffiti's support."),
            new DataPinDefinition(DataPinValueType.String,
                "current-fn",
                "The current function of the graffiti's support."),
            new DataPinDefinition(DataPinValueType.Boolean,
                "indoor",
                "True if the graffiti is indoor."),
            new DataPinDefinition(DataPinValueType.String,
                "material",
                "The material of the graffiti's support."),
            new DataPinDefinition(DataPinValueType.Decimal,
                "w",
                "The graffiti's width."),
            new DataPinDefinition(DataPinValueType.Decimal,
                "h",
                "The graffiti's height."),
            new DataPinDefinition(DataPinValueType.Decimal,
                "date-value",
                "The graffiti's date value."),
            new DataPinDefinition(DataPinValueType.String,
                "feature",
                "The graffiti's features.",
                "M"),
            new DataPinDefinition(DataPinValueType.Integer,
                "yr-last-seen",
                "The graffiti's last seen year."),
            new DataPinDefinition(DataPinValueType.String,
                "state",
                "The graffiti's recorded states types.",
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

        sb.Append("[GrfSummary]");

        if (Place != null) sb.Append(Place.GetFullName());

        if (!string.IsNullOrEmpty(SupportType))
        {
            if (sb.Length > 0) sb.Append(": ");
            sb.Append(SupportType);
        }
        if (Indoor) sb.Append('*');

        if (Size != null)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(Size);
        }

        return sb.ToString();
    }
}
