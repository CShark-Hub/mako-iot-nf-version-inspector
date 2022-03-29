using Xunit;
using Xunit.Abstractions;

namespace Mako.IoT.NfVersionInspector.Test
{
    public class DeviceExplorerTest
    {
        private readonly ITestOutputHelper _output;

        public DeviceExplorerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GetNativeAssembliesFromDevice()
        {
            // var result = DeviceExplorer.GetBoardInfo("COM5");
            //
            // foreach (var package in result)
            // {
            //     _output.WriteLine($"{package.Id} {package.Version} {package.Checksum}");
            // }
            //
            // Assert.NotEmpty(result);
        }
    }
}
