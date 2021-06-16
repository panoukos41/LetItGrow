using CouchDB.Driver;
using CouchDB.Driver.Exceptions;
using CouchDB.Driver.Types;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

#pragma warning disable CA1050 // Declare types in namespaces

/// <summary>
/// Class that holds all collection definition identifiers.
/// </summary>
public static class Collections
{
    public const string Main = "main";
}

[CollectionDefinition(Collections.Main)]
public class MainCollection : ICollectionFixture<MainFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

// This class will be initialized before any tests run and will be disposed when tests finish.
/// <summary>
/// For this class to work correctly dont forget to add [Collection("Db")] to your test class.
/// </summary>
public class MainFixture : IAsyncLifetime
{
    private static readonly IHost host;

    private static readonly IConfiguration configuration;

    private static readonly string databaseName;

    public static IServiceScope CreateScope() =>
        host.Services.CreateScope();

    static MainFixture()
    {
        host = new HostBuilder()
            .ConfigureCoreHost(Array.Empty<string>())
            .ConfigureServices((host, services) =>
            {
                services.AddCoreHostServices();

                // Remove user service and moq it.
                Remove<IUserService>(services);

                services.AddSingleton<IUserService>(s =>
                {
                    var moq = new Mock<IUserService>();

                    moq.Setup(x => x.GetId()).Returns("test-user");

                    return moq.Object;
                });

                services.AddMicroserviceCore(host.Configuration);
                services.AddMicroserviceInfrastructure(host.Configuration);
            })
            .UseEnvironment("Development")
            .Build();

        configuration = host.Services.GetRequiredService<IConfiguration>();
        databaseName = configuration.GetCouchDbName() ?? $"test_database_{Guid.NewGuid()}";
    }

    #region Helpers

    #region All

    public static Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        return CreateScope().ServiceProvider.GetRequiredService<ISender>().Send(request);
    }

    public static string String(int length, char character = 'a')
    {
        return string.Concat(Enumerable.Repeat(character, length));
    }

    public static void Remove<T>(IServiceCollection services)
    {
        services.Remove(services.First(x => x.ServiceType == typeof(T)));
    }

    public static string NewId()
    {
        return Path.GetRandomFileName().Remove(8, 1);
    }

    public static DateTimeOffset GetNowUtc()
    {
        return host.Services
            .GetRequiredService<IClockService>()
            .GetNowUtc();
    }

    #endregion

    #region Node Helpers

    public static CreateNode CreateNode(string name = "test-node", string? desc = null, NodeType type = NodeType.Measurement, JsonDocument? settings = null) =>
        new()
        {
            Name = name,
            Description = desc,
            Type = type,
            Settings = settings
        };

    public static CreateNode CreateIrrigationNode(string name = "test-node", string? desc = null, IrrigationSettings? settings = null) =>
        CreateNode(name, desc, NodeType.Irrigation, settings.ToJsonDocument());

    public static CreateNode CreateMeasurementNode(string name = "test-node", string? desc = null, MeasurementSettings? settings = null) =>
        CreateNode(name, desc, NodeType.Measurement, settings.ToJsonDocument());

    public static UpdateNode UpdateNode(NodeModel node, string? name = null, string? desc = null, JsonDocument? settings = null) =>
        new()
        {
            Id = node.Id,
            ConcurrencyStamp = node.ConcurrencyStamp,
            Type = node.Type,
            Name = name,
            Description = desc,
            Settings = settings
        };

    public static GroupAdd AddNodeToGroup(NodeModel node, GroupModel group) =>
        new()
        {
            Id = node.Id,
            ConcurrencyStamp = node.ConcurrencyStamp,
            GroupId = group.Id
        };

    #endregion

    #region Group Helpers

    public static CreateGroup CreateGroup(string name = "test-group", string? desc = null, GroupType type = default) =>
        new()
        {
            Name = name,
            Description = desc,
            Type = type
        };

    public static UpdateGroup UpdateGroup(GroupModel group, string? name = null, string? desc = null) =>
        new()
        {
            Id = group.Id,
            ConcurrencyStamp = group.ConcurrencyStamp,
            Name = name,
            Description = desc
        };

    #endregion

    #endregion

    #region Lifecycle

    public async Task InitializeAsync()
    {
        var client = host.Services.GetRequiredService<ICouchClient>();
        await client.GetOrCreateDatabaseAsync<CouchDocument>(databaseName);

        var db = client.GetDatabase<ViewDocument>(databaseName);
        var docs = new ViewDocument
        {
            Id = "_design/letitgrow",
            Views = new()
            {
                {
                    "node",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.node') {\n    emit(doc._id, doc._rev);\n  }\n}"
                    }
                },
                {
                    "node-group_id",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.node') {\n    emit(doc.group_id, doc._rev);\n  }\n}"
                    }
                },
                {
                    "node-connected",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.node') {\n    emit(doc.connected, doc._rev);\n  }\n}"
                    }
                },
                {
                    "group",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.group') {\n    emit(doc._id, doc._rev);\n  }\n}"
                    }
                },
                {
                    "nodeauth",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.nodeauth') {\n    emit(doc._id, doc._rev);\n  }\n}"
                    }
                },
                {
                    "nodeauth-node_id",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.nodeauth') {\n    emit(doc.node_id, doc._rev);\n  }\n}"
                    }
                },
                {
                    "irrigation-node_id",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.irrigation') {\n    emit(doc.node_id, doc._rev);\n  }\n}"
                    }
                },
                {
                    "irrigation-issued_at",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.irrigation') {\n    emit(doc.issued_at, doc._rev);\n  }\n}"
                    }
                },
                {
                    "measurement-node_id",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.measurement') {\n    emit(doc.node_id, doc._rev);\n  }\n}"
                    }
                },
                {
                    "measurement-measured_at",
                    new
                    {
                        map = "function (doc) {\n  if (doc.split_discriminator == 'letitgrow.measurement') {\n    emit(doc.measured_at, doc._rev);\n  }\n}"
                    }
                },
            }
        };

        try { await db.AddAsync(docs); }
        catch (CouchConflictException) { }
        catch { throw; }
    }

    private class ViewDocument : CouchDocument
    {
        [JsonProperty("views")]
        public Dictionary<string, object> Views { get; set; } = new();

        [JsonProperty("language")]
        public string Language { get; set; } = "javascript";
    }

    public async Task DisposeAsync()
    {
        if (configuration.GetValue("DeleteTestDatabase", true))
        {
            var client = host.Services.GetRequiredService<ICouchClient>();

            try { await client.DeleteDatabaseAsync(databaseName); }
            catch { }
        }
    }

    #endregion
}