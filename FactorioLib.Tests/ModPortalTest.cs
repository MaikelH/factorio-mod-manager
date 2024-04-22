using System.Threading.Tasks;
using FactorioLib.Types;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace FactorioLib.Tests;

[TestClass]
[TestSubject(typeof(ModPortal))]
public class ModPortalTest
{
    [TestMethod]
    public async Task TestGetMods()
    {
        ModPortal modPortal = new ModPortal();
        modPortal.Should().NotBeNull();

        var mods = await modPortal.GetMods(new ModsRequestParameters());

        mods.Should().NotBeNull();
        mods.Pagination.Should().NotBeNull();
        mods.Pagination.Count.Should().BeGreaterThan(0);
        mods.Results.Count.Should().BeGreaterThan(0);
    }
    
    [TestMethod]
    public async Task TestGetModsParameters()
    {
        ModPortal modPortal = new ModPortal();
        modPortal.Should().NotBeNull();

        var mods = await modPortal.GetMods(new ModsRequestParameters(){Page = 2});

        mods.Should().NotBeNull();
        mods.Pagination.Should().NotBeNull();
        mods.Pagination.Count.Should().BeGreaterThan(0);
        mods.Results.Count.Should().BeGreaterThan(0);
    }
}