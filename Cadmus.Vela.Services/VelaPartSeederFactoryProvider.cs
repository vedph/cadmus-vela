﻿using Cadmus.Core.Config;
using Cadmus.Seed;
using Cadmus.Seed.Epigraphy.Parts;
using Cadmus.Seed.General.Parts;
using Cadmus.Seed.Geo.Parts;
using Cadmus.Seed.Philology.Parts;
using Cadmus.Seed.Vela.Parts;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace Cadmus.Vela.Services;

/// <summary>
/// VeLA part seeders provider.
/// </summary>
/// <seealso cref="IPartSeederFactoryProvider" />
public sealed class VelaPartSeederFactoryProvider : IPartSeederFactoryProvider
{
    private static IHost GetHost(string config)
    {
        // build the tags to types map for parts/fragments
        Assembly[] seedAssemblies =
        [
            // Cadmus.Seed.General.Parts
            typeof(NotePartSeeder).Assembly,
            // Cadmus.Seed.Philology.Parts
            typeof(ApparatusLayerFragmentSeeder).Assembly,
            // Cadmus.Seed.Epigraphy.Parts
            typeof(EpiLigaturesLayerFragmentSeeder).Assembly,
            // Cadmus.Seed.Geo.Parts
            typeof(AssertedLocationsPartSeeder).Assembly,
            // Cadmus.Seed.Vela.Parts
            typeof(GrfLocalizationPartSeeder).GetTypeInfo().Assembly,
        ];
        TagAttributeToTypeMap map = new();
        map.Add(seedAssemblies);

        return new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                PartSeederFactory.ConfigureServices(services,
                    new StandardPartTypeProvider(map),
                    seedAssemblies);
            })
            // extension method from Fusi library
            .AddInMemoryJson(config)
            .Build();
    }

    /// <summary>
    /// Gets the part/fragment seeders factory.
    /// </summary>
    /// <param name="profile">The profile.</param>
    /// <returns>Factory.</returns>
    /// <exception cref="ArgumentNullException">profile</exception>
    public PartSeederFactory GetFactory(string profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        return new PartSeederFactory(GetHost(profile));
    }
}
