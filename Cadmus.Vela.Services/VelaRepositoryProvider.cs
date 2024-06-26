﻿using System.Reflection;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.Mongo;
using Cadmus.General.Parts;
using Cadmus.Philology.Parts;
using System;
using Cadmus.Vela.Parts;
using Cadmus.Epigraphy.Parts;
using Cadmus.Geo.Parts;
using Fusi.Tools.Configuration;

namespace Cadmus.Vela.Services;

/// <summary>
/// Cadmus VeLA repository provider.
/// Tag: <c>repository-provider.vela</c>.
/// </summary>
/// <seealso cref="IRepositoryProvider" />
[Tag("repository-provider.vela")]
public sealed class VelaRepositoryProvider : IRepositoryProvider
{
    private readonly IPartTypeProvider _partTypeProvider;

    /// <summary>
    /// The connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="__PRJ__RepositoryProvider"/>
    /// class.
    /// </summary>
    public VelaRepositoryProvider()
    {
        ConnectionString = "";
        TagAttributeToTypeMap map = new();
        map.Add(
        [
            // Cadmus.General.Parts
            typeof(NotePart).GetTypeInfo().Assembly,
            // Cadmus.Philology.Parts
            typeof(ApparatusLayerFragment).GetTypeInfo().Assembly,
            // Cadmus.Epigraphy.Parts
            typeof(EpiSupportPart).GetTypeInfo().Assembly,
            // Cadmus.Geo.Parts
            typeof(AssertedLocationsPart).GetTypeInfo().Assembly,
            // Cadmus.Vela.Parts
            typeof(GrfLocalizationPart).GetTypeInfo().Assembly,
        ]);

        _partTypeProvider = new StandardPartTypeProvider(map);
    }

    /// <summary>
    /// Gets the part type provider.
    /// </summary>
    /// <returns>part type provider</returns>
    public IPartTypeProvider GetPartTypeProvider()
    {
        return _partTypeProvider;
    }

    /// <summary>
    /// Creates a Cadmus repository.
    /// </summary>
    /// <returns>repository</returns>
    public ICadmusRepository CreateRepository()
    {
        // create the repository (no need to use container here)
        MongoCadmusRepository repository = new(_partTypeProvider,
                new StandardItemSortKeyBuilder());

        repository.Configure(new MongoCadmusRepositoryOptions
        {
            ConnectionString = ConnectionString ??
            throw new InvalidOperationException(
                "No connection string set for IRepositoryProvider implementation")
        });

        return repository;
    }
}
