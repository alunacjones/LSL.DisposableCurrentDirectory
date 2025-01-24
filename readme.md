[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-disposablecurrentdirectory.svg)](https://ci.appveyor.com/project/alunacjones/lsl-disposablecurrentdirectory)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.DisposableCurrentDirectory)](https://coveralls.io/github/alunacjones/LSL.DisposableCurrentDirectory)
[![NuGet](https://img.shields.io/nuget/v/LSL.DisposableCurrentDirectory.svg)](https://www.nuget.org/packages/LSL.DisposableCurrentDirectory/)

# LSL.DisposableCurrentDirectory

Provides a disposable current directory via the `Directory.GetCurrentDirectory` and `Directory.SetCurrentDirectory` methods.

```csharp
using LSL.DisposableCurrentDirectory;
...

var original = Directory.GetCurrentDirectory();

using (var directory = new DisposableCurrentDirectory(@"C:\my-test"))
{
    var newDirectory = Directory.GetCurrentDirectory();
    // newDirectory will be "C:\my-test"
}

var directory = Directory.GetCurrentDirectory();
// directory will now be the same value as the 'original' variable
```
