using Cadmus.Core;
using Cadmus.Mat.Bricks;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Vela.Parts;

/// <summary>
/// Graffiti summary part.
/// <para>Tag: <c>it.vedph.graffiti.frame</c>.</para>
/// </summary>
[Tag("it.vedph.graffiti.frame")]
public sealed class GrfFramePart : PartBase
{
    /// <summary>
    /// Gets or sets the figure description.
    /// </summary>
    public string? Figure { get; set; }

    /// <summary>
    /// Gets or sets the frame description.
    /// </summary>
    public string? Frame { get; set; }

    /// <summary>
    /// Gets or sets the size.
    /// </summary>
    public PhysicalSize? Size { get; set; }

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

        if (Size != null)
        {
            if (Size.W != null) builder.AddValue("w", Size.W.Value);
            if (Size.H != null) builder.AddValue("h", Size.H.Value);
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
            new DataPinDefinition(DataPinValueType.Decimal,
                "w",
                "The graffiti's width."),
            new DataPinDefinition(DataPinValueType.Decimal,
                "h",
                "The graffiti's height."),
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
        sb.Append("[GrfFrame]");

        if (Size != null) sb.Append(": ").Append(Size);
        return sb.ToString();
    }
}
