using Bogus;
using Cadmus.Core;
using Cadmus.Mat.Bricks;
using Cadmus.Vela.Parts;
using Fusi.Tools.Configuration;
using System;

namespace Cadmus.Seed.Vela.Parts;

/// <summary>
/// Seeder for <see cref="GrfFramePart"/>.
/// Tag: <c>seed.it.vedph.graffiti.frame</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.graffiti.frame")]
public sealed class GrfFramePartSeeder : PartSeederBase
{
    /// <summary>
    /// Creates and seeds a new part.
    /// </summary>
    /// <param name="item">The item this part should belong to.</param>
    /// <param name="roleId">The optional part role ID.</param>
    /// <param name="factory">The part seeder factory. This is used
    /// for layer parts, which need to seed a set of fragments.</param>
    /// <returns>A new part or null.</returns>
    /// <exception cref="ArgumentNullException">item or factory</exception>
    public override IPart? GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        ArgumentNullException.ThrowIfNull(item);

        GrfFramePart part = new Faker<GrfFramePart>()
           .RuleFor(p => p.Figure, f => f.Lorem.Sentence())
           .RuleFor(p => p.Frame, f => f.Lorem.Sentence())
           .RuleFor(p => p.Size,
                f => new PhysicalSize
                {
                    W = new PhysicalDimension
                    {
                        Value = f.Random.Number(5, 30),
                        Unit = "cm"
                    },
                    H = new PhysicalDimension
                    {
                        Value = f.Random.Number(5, 30),
                        Unit = "cm"
                    }
                })
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
