using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;
using Cadmus.Refs.Bricks;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfWritingPartTest
{
    private static GrfWritingPart GetPart()
    {
        GrfWritingPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfWritingPart)seeder.GetPart(item, null, null)!;
    }

    private static GrfWritingPart GetEmptyPart()
    {
        return new GrfWritingPart
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
        GrfWritingPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfWritingPart part2 = TestHelper.DeserializePart<GrfWritingPart>(json)!;

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
        GrfWritingPart part = GetEmptyPart();
        part.System = "latn";
        part.Languages.Add("grc");
        part.Languages.Add("lat");
        part.Scripts.Add("gothic");
        part.Counts.Add(new DecoratedCount
        {
            Id = "row",
            Value = 12
        });
        part.ScriptFeatures.Add("sf");
        part.LetterFeatures.Add("lf");
        part.HasPoetry = true;
        part.HasProse = true;
        part.Metres.Add("7s");
        part.Metres.Add("8s");

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(13, pins.Count);

        // system
        DataPin? pin = pins.Find(p => p.Name == "system" && p.Value == "latn");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // language
        pin = pins.Find(p => p.Name == "language" && p.Value == "grc");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "language" && p.Value == "lat");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // script
        pin = pins.Find(p => p.Name == "script" && p.Value == "gothic");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // c-row
        pin = pins.Find(p => p.Name == "c-row" && p.Value == "12");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // ruling
        pin = pins.Find(p => p.Name == "ruling" && p.Value == "0");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // rubrics
        pin = pins.Find(p => p.Name == "rubrics" && p.Value == "0");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // features
        pin = pins.Find(p => p.Name == "script-feature" && p.Value == "sf");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "letter-feature" && p.Value == "lf");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // poetry
        pin = pins.Find(p => p.Name == "poetry" && p.Value == "1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // prose
        pin = pins.Find(p => p.Name == "prose" && p.Value == "1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // metre
        pin = pins.Find(p => p.Name == "metre" && p.Value == "7s");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "metre" && p.Value == "8s");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
