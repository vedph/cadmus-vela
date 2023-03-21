using Bogus;
using Cadmus.Core;
using Cadmus.Vela.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Vela.Parts;

/// <summary>
/// Seeder for <see cref="GrfFigurativePart"/>.
/// Tag: <c>seed.it.vedph.graffiti.figurative</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.graffiti.figurative")]
public sealed class GrfFigurativePartSeeder : PartSeederBase
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
        if (item == null) throw new ArgumentNullException(nameof(item));

        GrfFigurativePart part = new Faker<GrfFigurativePart>()
            .RuleFor(p => p.FrameType, f => f.PickRandom("square", "rect"))
            .RuleFor(p => p.Type, f => f.PickRandom("ani", "obj"))
            .RuleFor(p => p.Features, f => new List<string>
            {
                f.PickRandom("cross", "game")
            })
            .RuleFor(p => p.Note, f => f.Lorem.Sentence())
            .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
