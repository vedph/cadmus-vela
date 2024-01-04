using Bogus;
using Cadmus.Core;
using Cadmus.Vela.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Vela.Parts;

/// <summary>
/// Seeder for <see cref="GrfStatesPart"/>.
/// Tag: <c>seed.it.vedph.graffiti.states</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.graffiti.states")]
public sealed class GrfStatesPartSeeder : PartSeederBase
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

        GrfStatesPart part = new Faker<GrfStatesPart>()
           .RuleFor(p => p.States, f => new List<GrfState>
           {
               new()
               {
                   Type = "s" + f.Random.Number(0, 3),
                   Date = f.Date.Past(3),
                   Reporter = f.Name.FullName(),
                   Note = f.Lorem.Sentence()
               }
           })
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
