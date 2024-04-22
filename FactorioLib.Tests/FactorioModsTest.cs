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
        var mods = new FactorioMods("./mods");

        var list = mods.ListModFiles();

        list.Should().HaveCount(2);
    }

    [TestMethod]
    public void TestListMethod()
    {
        var mods = new FactorioMods("./mods");

        var list = mods.List();

        list.Should().HaveCountGreaterThan(2);
    }
}