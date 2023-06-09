﻿using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Vela.Parts;

namespace Cadmus.Vela.Parts.Test;

public sealed class GrfFigurativePartTest
{
    private static GrfFigurativePart GetPart()
    {
        GrfFigurativePartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (GrfFigurativePart)seeder.GetPart(item, null, null)!;
    }

    private static GrfFigurativePart GetEmptyPart()
    {
        return new GrfFigurativePart
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
        GrfFigurativePart part = GetPart();

        string json = TestHelper.SerializePart(part);
        GrfFigurativePart part2 = TestHelper.DeserializePart<GrfFigurativePart>(json)!;

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
        GrfFigurativePart part = GetEmptyPart();
        part.Types = new List<string> { "animal", "man" };

        List<DataPin> pins = part.GetDataPins(null).ToList();
        Assert.Equal(2, pins.Count);

        // type
        DataPin? pin = pins.Find(p => p.Name == "type" && p.Value == "animal");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        pin = pins.Find(p => p.Name == "type" && p.Value == "man");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
    }
}
