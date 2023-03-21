using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;

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
        // TODO: set only the properties required for pins
        // in a predictable way so we can test them

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Single(pins);

        DataPin? pin = pins.Find(p => p.Name == "id" && p.Value == "steph");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
