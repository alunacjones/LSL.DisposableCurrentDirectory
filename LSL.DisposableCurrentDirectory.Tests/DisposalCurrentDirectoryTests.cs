using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace LSL.DisposableCurrentDirectory.Tests;

public class DisposableCurrentDirectoryTests
{
    [Test]
    public void GivenAValidRootPath_ThenItShouldUseADisposableCurrentDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        // The test csproj file creates this output folder so it doesn't fall over
        var newCurrentDirectory = Path.Combine(currentDirectory, "a-test");

        using (var directory = new DisposableCurrentDirectory(newCurrentDirectory))
        {
            Directory.GetCurrentDirectory().Should().Be(newCurrentDirectory);
            directory.CurrentDirectory.Should().Be(newCurrentDirectory);
            directory.OriginalCurrentDirectory.Should().Be(currentDirectory);
        }

        Directory.GetCurrentDirectory().Should().Be(currentDirectory);
    }

    [Test]
    public void GivenAValidRelativePath_ThenItShouldUseADisposableCurrentDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        // The test csproj file creates this output folder so it doesn't fall over
        var newCurrentDirectory = "a-test";
        var rootedCurrentDirectory = Path.Combine(currentDirectory, newCurrentDirectory);

        using (var directory = new DisposableCurrentDirectory(newCurrentDirectory))
        {
            Directory.GetCurrentDirectory().Should().Be(rootedCurrentDirectory);
            directory.CurrentDirectory.Should().Be(rootedCurrentDirectory);
            directory.OriginalCurrentDirectory.Should().Be(currentDirectory);
        }

        Directory.GetCurrentDirectory().Should().Be(currentDirectory);
    }

    [Test]
    public void GivenAnInvalidRootedPath_ThenItShouldUseADisposableCurrentDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var newCurrentDirectory = Path.Combine(currentDirectory, "a-breaking-test");

        new Action(() => new DisposableCurrentDirectory(newCurrentDirectory))
            .Should()
            .Throw<DirectoryNotFoundException>();
    }

    [Test]
    public void GivenAnInvalidRelativePath_ThenItShouldUseADisposableCurrentDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var newCurrentDirectory = "a-breaking-test";

        new Action(() => new DisposableCurrentDirectory(newCurrentDirectory))
            .Should()
            .Throw<DirectoryNotFoundException>();
    }
}