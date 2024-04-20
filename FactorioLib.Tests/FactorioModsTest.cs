using FactorioLib;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FactorioLib.Tests;

[TestClass]
[TestSubject(typeof(FactorioMods))]
public class FactorioModsTest
{

    [TestMethod]
    public void TestGetMods()
    {
        var mods = new FactorioMods("/home/maikel/RiderProjects/FactorioModUpdater/FactorioLib.Tests/mods");

        var list = mods.ListModFiles();

        list.Should().HaveCount(2);
    }
}