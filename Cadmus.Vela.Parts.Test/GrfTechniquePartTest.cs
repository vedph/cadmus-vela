using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfTechniquePartTest
{
    private static GrfTechniquePart GetPart()
    {
        GrfTechniquePartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfTechniquePart)seeder.GetPart(item, null, null)!;
    }

    private static GrfTechniquePart GetEmptyPart()
    {
        return new GrfTechniquePart
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
        GrfTechniquePart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfTechniquePart part2 = TestHelper.DeserializePart<GrfTechniquePart>(json)!;

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
        GrfTechniquePart part = GetEmptyPart();
        for (int n = 1; n <= 2; n++)
        {
            part.Techniques.Add("tec" + n);
            part.Tools.Add("tol" + n);
        }

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(4, pins.Count);

        DataPin? pin;
        for (int n = 1; n <= 2; n++)
        {
            // technique
            pin = pins.Find(p => p.Name == "technique" && p.Value == "tec" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            // tool
            pin = pins.Find(p => p.Name == "tool" && p.Value == "tol" + n);
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
