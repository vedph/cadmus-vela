using Cadmus.Core;
using Cadmus.Vela.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Vela.Parts.Test;

public sealed class GrfLocalizationPartSeederTest
{
    private static readonly PartSeederFactory _factory =
        TestHelper.GetFactory();
    private static readonly SeedOptions _seedOptions =
        _factory.GetSeedOptions();
    private static readonly IItem _item =
        _factory.GetItemSeeder().GetItem(1, "facet");

    [Fact]
    public void TypeHasTagAttribute()
    {
        Type t = typeof(GrfLocalizationPartSeeder);
        TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.it.vedph.graffiti.localization", attr!.Tag);
    }

    [Fact]
    public void Seed_Ok()
    {
        GrfLocalizationPartSeeder seeder = new();
        seeder.SetSeedOptions(_seedOptions);

        IPart? part = seeder.GetPart(_item, null, _factory);

        Assert.NotNull(part);

        GrfLocalizationPart? p = part as GrfLocalizationPart;
        Assert.NotNull(p);

        TestHelper.AssertPartMetadata(p!);
    }
}
