using Cadmus.Core;
using Cadmus.Seed.Vela.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfStatesPartTest
{
    private static GrfStatesPart GetPart()
    {
        GrfStatesPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfStatesPart)seeder.GetPart(item, null, null)!;
    }

    private static GrfStatesPart GetEmptyPart()
    {
        return new GrfStatesPart
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
        GrfStatesPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfStatesPart part2 = TestHelper.DeserializePart<GrfStatesPart>(json)!;

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
        GrfStatesPart part = GetEmptyPart();
        for (int n = 1; n <= 3; n++)
        {
            part.States.Add(new GrfState
            {
                Type = "s" + n
            });
        }

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(4, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count" && p.Value == "3");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        for (int n = 1; n <= 3; n++)
        {
            pin = pins.Find(p => p.Name == "type" && p.Value == "s" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
