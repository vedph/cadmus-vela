using Bogus;
using Cadmus.Core;
using Cadmus.Mat.Bricks;
using Cadmus.Refs.Bricks;
using Cadmus.Vela.Parts;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Vela.Parts;

/// <summary>
/// Seeder for <see cref="GrfSummaryPart"/>.
/// Tag: <c>seed.it.vedph.graffiti.summary</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.graffiti.summary")]
public sealed class GrfSummaryPartSeeder : PartSeederBase
{
    static private ProperName GetPlace(Faker faker)
    {
        ProperName name = new()
        {
            Language = "ita",
        };
        name!.Pieces!.Add(new ProperNamePiece
        {
            Type = "sestriere",
            Value = faker.PickRandom("dd", "gd")
        });
        name!.Pieces!.Add(new ProperNamePiece
        {
            Type = "location",
            Value = faker.Address.StreetName() + ", " + faker.Random.Number(1, 50)
        });
        return name;
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
        if (item == null) throw new ArgumentNullException(nameof(item));

        string[] fnn = new[] { "street", "house" };
        DateTime lastSeen = new Faker().Date.Past();

        GrfSummaryPart part = new Faker<GrfSummaryPart>()
           .RuleFor(p => p.Place, f => GetPlace(f))
           .RuleFor(p => p.SupportType, f => f.PickRandom("wall", "door"))
           .RuleFor(p => p.ObjectType, f => f.PickRandom("street", "bridge", "well"))
           .RuleFor(p => p.OriginalFn, f => f.PickRandom(fnn))
           .RuleFor(p => p.CurrentFn, f => f.PickRandom(fnn))
           .RuleFor(p => p.Indoor, f => f.Random.Bool())
           .RuleFor(p => p.Material,
                f => f.PickRandom("concrete", "wood", "stone"))
           .RuleFor(p => p.Description, f => f.Lorem.Sentence())
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
           .RuleFor(p => p.Date,
                f => HistoricalDate.Parse($"{f.Random.Number(1500, 1900)} AD"))
           .RuleFor(p => p.Features, f => new List<string> 
           {
               f.PickRandom("text", "digit")
           })
           .RuleFor(p => p.FigDescription, f => f.Lorem.Sentence())
           .RuleFor(p => p.FrameDescription, f => f.Lorem.Sentence())
           .RuleFor(p => p.LastSeen, lastSeen)
           .RuleFor(p => p.States, f => new List<GrfState>
           {
               new GrfState
               {
                   Type = "s" + f.Random.Number(0, 3),
                   Date = lastSeen,
                   Reporter = f.Name.FullName(),
                   Note = f.Lorem.Sentence()
               }
           })
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
