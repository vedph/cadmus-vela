using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;
using Cadmus.Mat.Bricks;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfFramePartTest
{
    private static GrfFramePart GetPart()
    {
        GrfFramePartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfFramePart)seeder.GetPart(item, null, null)!;
    }

    private static GrfFramePart GetEmptyPart()
    {
        return new GrfFramePart
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
        GrfFramePart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfFramePart part2 = TestHelper.DeserializePart<GrfFramePart>(json)!;

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
        GrfFramePart part = GetEmptyPart();
        part.Size = new PhysicalSize
        {
            W = new PhysicalDimension
            {
                Value = 10,
                Unit = "cm"
            },
            H = new PhysicalDimension
            {
                Value = 5,
                Unit = "mm"
            },
        };

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(2, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "w" && p.Value == "10");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "h" && p.Value == "0.5");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
