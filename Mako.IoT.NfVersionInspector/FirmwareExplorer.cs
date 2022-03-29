
namespace Mako.IoT.NFVersionInspector
{
    public class FirmwareExplorer
    {
        public static IEnumerable<Package> GetNativePackages()
        {
            return new Package[]
            {
                new Package("mscorlib", "100.5.0.14", "0xC5322585"),
                new Package("nanoFramework.Runtime.Native", "100.0.8.0", "0x2307A8F3"),
                new Package("nanoFramework.Hardware.Esp32", "100.0.7.3", "0xBE7FF253"),
                new Package("nanoFramework.Hardware.Esp32.Rmt", "100.0.3.0", "0x9A53BB44"),
                new Package("nanoFramework.Devices.OneWire", "100.0.3.4", "0xA5C172BD"),
                new Package("nanoFramework.Networking.Sntp", "100.0.4.4", "0xE2D9BDED"),
                new Package("nanoFramework.ResourceManager", "100.0.0.1", "0xDCD7DF4D"),
                new Package("nanoFramework.System.Collections", "100.0.0.1", "0x5A31313D"),
                new Package("nanoFramework.System.Text", "100.0.0.1", "0x8E6EB73D"),
                new Package("nanoFramework.Runtime.Events", "100.0.8.0", "0x0EAB00C9"),
                new Package("EventSink", "1.0.0.0", "0xF32F4C3E"),
                new Package("System.IO.FileSystem", "1.0.0.0", "0x210D05B1"),
                new Package("System.Math", "100.0.5.4", "0x46092CB1"),
                new Package("System.Net", "100.1.3.4", "0xC74796C2"),
                new Package("Windows.Devices.Adc", "100.1.3.3", "0xCA03579A"),
                new Package("System.Device.Adc", "100.0.0.0", "0xE5B80F0B"),
                new Package("System.Device.Dac", "100.0.0.6", "0x02B3E860"),
                new Package("System.Device.Gpio", "100.1.0.4", "0xB6D0ACC1"),
                new Package("Windows.Devices.Gpio", "100.1.2.2", "0xC41539BE"),
                new Package("Windows.Devices.I2c", "100.2.0.2", "0x79EDBF71"),
                new Package("System.Device.I2c", "100.0.0.1", "0xFA806D33"),
                new Package("Windows.Devices.Pwm", "100.1.3.3", "0xBA2E2251"),
                new Package("System.Device.Pwm", "100.1.0.4", "0xABF532C3"),
                new Package("Windows.Devices.SerialCommunication", "100.1.1.2", "0x34BAF06E"),
                new Package("System.IO.Ports", "100.1.2.0", "0x564F2452"),
                new Package("Windows.Devices.Spi", "100.1.4.2", "0x360239F1"),
                new Package("System.Device.Spi", "100.1.0.0", "0x48031DC5"),
                new Package("Windows.Devices.Wifi", "100.0.6.2", "0xA94A849E"),
                new Package("Windows.Storage", "100.0.2.0", "0x954A4192"),
            };
        }
    }
}
