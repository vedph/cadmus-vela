﻿using Bogus;
using Cadmus.Core;
using Cadmus.Vela.Parts;
using Fusi.Tools.Configuration;
using System;

namespace Cadmus.Seed.Vela.Parts;

/// <summary>
/// Seeder for <see cref="GrfSummaryPart"/>.
/// Tag: <c>seed.it.vedph.graffiti.support</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.graffiti.support")]
public sealed class GrfSupportPartSeeder : PartSeederBase
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

        GrfSupportPart part = new Faker<GrfSupportPart>()
           .RuleFor(p => p.Type, f => f.PickRandom("wall", "door"))
           .RuleFor(p => p.Material,
                f => f.PickRandom("concrete", "wood", "stone"))
           .RuleFor(p => p.Note, f => f.Lorem.Sentence())
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
