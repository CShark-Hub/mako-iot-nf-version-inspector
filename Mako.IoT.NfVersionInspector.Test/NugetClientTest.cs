using System.Linq;
using Mako.IoT.NFVersionInspector;
using Mako.IoT.NFVersionInspector.Services;
using Xunit;

namespace Mako.IoT.NfVersionInspector.Test
{
    public class NugetClientTest
    {
        [Fact]
        public void GetFromNuspec()
        {
            var parser = new NuspecParser();
            var result = new NugetClient(parser).GetFromNuspec("nanoFramework.Windows.Storage", "1.4.5-preview.30", out var versionFound);

            Assert.Equal("nanoFramework.Windows.Storage", result.Id);
            Assert.Equal("1.4.5-preview.30", result.Version);
            Assert.False(result.IsNative);
            Assert.Equal(5, result.Dependencies.Count);
            Assert.Contains(result.Dependencies,
                p => p.Id == "nanoFramework.Windows.Storage" && p.IsNative && p.Version == "100.0.2.0" && p.Checksum == "0x954A4192");
            Assert.Contains(result.Dependencies,
                p => p.Id == "nanoFramework.System.Text" && !p.IsNative && p.Version == "1.1.2");
        }

        [Fact]
        public void GetPackageVersions()
        {
            var parser = new NuspecParser();
            var result = new NugetClient(parser).GetPackageVersions("nanoFramework.Windows.Storage");

            Assert.True(result.Any());
        }
    }
}
