using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;
using Cadmus.Refs.Bricks;
using Cadmus.Mat.Bricks;
using Fusi.Antiquity.Chronology;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfSummaryPartTest
{
    private static GrfSummaryPart GetPart()
    {
        GrfSummaryPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfSummaryPart)seeder.GetPart(item, null, null)!;
    }

    private static GrfSummaryPart GetEmptyPart()
    {
        return new GrfSummaryPart
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
        GrfSummaryPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfSummaryPart part2 = TestHelper.DeserializePart<GrfSummaryPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);
    }

    [Fact]
    public void GetDataPins_Tag_1()
    {
        GrfSummaryPart part = GetEmptyPart();
        part.Place = new AssertedProperName
        {
            Language = "ita",
        };
        part.Place.Pieces!.Add(new ProperNamePiece
        {
            Type = "sestriere",
            Value = "Dorsoduro"
        });
        part.SupportType = "support";
        part.ObjectType = "object";
        part.OriginalFn = "original";
        part.CurrentFn = "current";
        part.Indoor = true;
        part.Material = "limestone";
        part.Size = new PhysicalSize
        {
            W = new PhysicalDimension
            {
                Value = 10,
                Unit = "cm"
            }
        };
        part.Date = HistoricalDate.Parse("1234 AD");
        part.Features.Add("fa");
        part.Features.Add("fb");
        part.LastSeen = new DateTime(2020, 1, 1);
        part.States.Add(new GrfSupportState
        {
            Type = "s1"
        });

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(13, pins.Count);

        // place
        DataPin? pin = pins.Find(p => p.Name == "place" && p.Value == "dorsoduro");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // support-type
        pin = pins.Find(p => p.Name == "support-type" && p.Value == "support");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // object-type
        pin = pins.Find(p => p.Name == "object-type" && p.Value == "object");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // original-fn
        pin = pins.Find(p => p.Name == "original-fn" && p.Value == "original");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // current-fn
        pin = pins.Find(p => p.Name == "current-fn" && p.Value == "current");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // indoor
        pin = pins.Find(p => p.Name == "indoor" && p.Value == "1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // material
        pin = pins.Find(p => p.Name == "material" && p.Value == "limestone");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // w
        pin = pins.Find(p => p.Name == "w" && p.Value == "10");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // date-value
        pin = pins.Find(p => p.Name == "date-value" && p.Value == "1234");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // feature's
        pin = pins.Find(p => p.Name == "feature" && p.Value == "fa");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        pin = pins.Find(p => p.Name == "feature" && p.Value == "fb");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // yr-last-seen
        pin = pins.Find(p => p.Name == "yr-last-seen" && p.Value == "2020");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        // state
        pin = pins.Find(p => p.Name == "state" && p.Value == "s1");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
