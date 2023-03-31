using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;
using Cadmus.Refs.Bricks;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfLocalizationPartTest
{
    private static GrfLocalizationPart GetPart()
    {
        GrfLocalizationPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfLocalizationPart)seeder.GetPart(item, null, null)!;
    }

    private static GrfLocalizationPart GetEmptyPart()
    {
        return new GrfLocalizationPart
        {
            ItemId = Guid.NewGuid().ToString(),
            RoleId = "some-role",
            CreatorId = "zeus",
            UserId = "another",
        };
    }

    [Fact]
    public void Part_Is_Serializable()
    {
        GrfLocalizationPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfLocalizationPart part2 =
            TestHelper.DeserializePart<GrfLocalizationPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);
    }

    [Fact]
    public void GetDataPins_Ok()
    {
        GrfLocalizationPart part = GetEmptyPart();
        part.Place = new ProperName
        {
            Language = "ita",
        };
        part.Place.Pieces!.Add(new ProperNamePiece
        {
            Type = "sestriere",
            Value = "Dorsoduro"
        });
        part.ObjectType = "object";
        part.Function = "fn";
        part.Indoor = true;

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(4, pins.Count);

        // place
        DataPin? pin = pins.Find(p => p.Name == "place" && p.Value == "dorsoduro");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // function
        pin = pins.Find(p => p.Name == "function" && p.Value == "fn");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // object-type
        pin = pins.Find(p => p.Name == "object-type" && p.Value == "object");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // indoor
        pin = pins.Find(p => p.Name == "indoor" && p.Value == "1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
