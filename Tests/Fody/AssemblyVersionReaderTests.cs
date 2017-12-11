using System.IO;
using Fody;
using Xunit;


public class AssemblyVersionReaderTests
{
    [Fact]
    public void BadImage()
    {
        var path = Path.Combine(AssemblyLocation.CurrentDirectory, "Fody/BadAssembly.dll");
        Assert.Throws<WeavingException>(() =>
        {
            AssemblyVersionReader.GetAssemblyVersion(path);
        });
    }
}