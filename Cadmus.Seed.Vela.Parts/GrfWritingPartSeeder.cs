using Bogus;
using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Cadmus.Vela.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Vela.Parts;

/// <summary>
/// Seeder for <see cref="GrfWritingPart"/>.
/// Tag: <c>seed.it.vedph.graffiti.writing</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.graffiti.writing")]
public sealed class GrfWritingPartSeeder : PartSeederBase
{
    private static List<DecoratedCount> GetCounts(Faker f)
    {
        return
        [
            new DecoratedCount
            {
                Id = "row",
                Value = f.Random.Number(1, 50)
            }
        ];
    }

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

        Faker faker = new();
        bool ruling = faker.Random.Bool();
        bool rubrics = faker.Random.Bool();

        GrfWritingPart part = new Faker<GrfWritingPart>()
            .RuleFor(p => p.System, f => f.PickRandom("latn", "grek"))
            .RuleFor(p => p.Languages, f => new List<string>
            {
                f.PickRandom("lat", "grc")
            })
            .RuleFor(p => p.Script, f => f.PickRandom("gothic", "merchant"))
            .RuleFor(p => p.Casing, f => f.PickRandom("upper", "lower"))
            .RuleFor(p => p.ScriptFeatures, f => new List<string>
            {
                f.PickRandom("punctuation", "ligature")
            })
            .RuleFor(p => p.LetterFeatures, f => new List<string>
            {
                f.PickRandom("monogram", "sigla")
            })
            .RuleFor(p => p.Counts, GetCounts)
            .RuleFor(p => p.HasRuling, ruling)
            .RuleFor(p => p.Ruling, f => ruling ? f.Lorem.Sentence() : null)
            .RuleFor(p => p.HasRubrics, rubrics)
            .RuleFor(p => p.Rubrics, f => rubrics ? f.Lorem.Sentence() : null)
            .RuleFor(p => p.HasProse, f => f.Random.Bool(0.75f))
            .RuleFor(p => p.HasPoetry, f => f.Random.Bool(0.25f))
            .RuleFor(p => p.Metres, f => new List<string>
            {
                f.PickRandom("-", "s7")
            })
            .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
