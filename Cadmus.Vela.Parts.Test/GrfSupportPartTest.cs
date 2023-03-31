using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfSupportPartTest
{
    private static GrfSupportPart GetPart()
    {
        GrfSupportPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfSupportPart)seeder.GetPart(item, null, null)!;
    }

    private static GrfSupportPart GetEmptyPart()
    {
        return new GrfSupportPart
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
        GrfSupportPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfSupportPart part2 = TestHelper.DeserializePart<GrfSupportPart>(json)!;

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
        GrfSupportPart part = GetEmptyPart();
        part.Type = "t";
        part.Material = "m";

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(2, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "type" && p.Value == "t");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "material" && p.Value == "m");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
