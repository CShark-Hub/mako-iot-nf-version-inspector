using System.Collections.Concurrent;

namespace Mako.IoT.NFVersionInspector
{
    public class Cache
    {
        private static readonly IDictionary<string, HashSet<Package>> Packages =
            new ConcurrentDictionary<string, HashSet<Package>>();

        public static Package GetOrAdd(string id, string version, Func<string, string, Package> getFunc, bool forceRefresh = false)
        {
            if (!Packages.ContainsKey(id))
            {
                Packages.Add(id, new HashSet<Package>(Storage.Load(id)));
            }

            if (forceRefresh)
                Packages[id].RemoveWhere(p => p.Version == version);
            
            var package = Packages[id].SingleOrDefault(p => p.Version == version);

            if (package == null)
            {
                package = getFunc(id, version);
                Packages[id].Add(package);

                Storage.Save(id, Packages[id]);
            }

            return package;
        }
    }
}
