using System.Collections.Generic;
using System.Linq;
using Mako.IoT.NFVersionInspector;
using Mako.IoT.NFVersionInspector.Services;
using Xunit;

namespace Mako.IoT.NfVersionInspector.Test
{
    public class StorageTest
    {
        [Fact]
        public void Save()
        {
            var p = new HashSet<Package>(new Package[]
            {
                new Package("id.1", "version.1", new List<Package>
                {
                    { new Package("id.2", "version.1") },
                    { new Package("id.3", "version.1") }
                }),
                new Package("id.1", "version.2"),
                new Package("id.1", "version.3"),
                new Package("id.1", "version.4"),
                new Package("id.1", "version.5"),
                new Package("id.1", "version.6"),
                new Package("id.1", "version.7"),
                new Package("id.1", "version.8"),
            });

            new Storage().Save("id.1", p);
        }

        [Fact]
        public void Load()
        {
            var p = new Storage().Load("id.1");
            Assert.Equal(8, p.Count());
        }
    }
}
