using Bogus;
using Cadmus.Core;
using Cadmus.Vela.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Vela.Parts;

/// <summary>
/// Seeder for <see cref="GrfTechniquePart"/>.
/// Tag: <c>seed.it.vedph.graffiti.technique</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.graffiti.technique")]
public sealed class GrfTechniquePartSeeder : PartSeederBase
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

        GrfTechniquePart part = new Faker<GrfTechniquePart>()
            .RuleFor(p => p.Techniques, f => new List<string>
            {
                f.PickRandom("charcoal", "graffiti")
            })
            .RuleFor(p => p.Tools, f => new List<string>
            {
                f.PickRandom("nail", "gradine")
            })
            .RuleFor(p => p.Note, f => f.Lorem.Sentence())
            .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
