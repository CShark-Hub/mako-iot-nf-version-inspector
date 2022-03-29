using System.Net;
using Newtonsoft.Json.Linq;

namespace Mako.IoT.NFVersionInspector
{
    public class NugetClient
    {
        public static Package GetFromNuspec(string id, string version, out string? versionFound)
        {
            versionFound = null;
            using var client = new HttpClient();
            var response = client.GetAsync($"https://api.nuget.org/v3-flatcontainer/{id}/{version}/{id}.nuspec").GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var versions = GetPackageVersions(id);
                versionFound = versions.FirstOrDefault(v => v.StartsWith(version));
                if (versionFound != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Package {id} {version} not found on nuget. Getting closest version: {versionFound}");
                    Console.ForegroundColor = ConsoleColor.White;
                    response = client
                        .GetAsync($"https://api.nuget.org/v3-flatcontainer/{id}/{versionFound}/{id}.nuspec")
                        .GetAwaiter().GetResult();
                }
            }

            response.EnsureSuccessStatusCode();
            using var r = new StreamReader(response.Content.ReadAsStream());
            return NuspecParser.Parse(r);
        }

        public static IEnumerable<string> GetPackageVersions(string id)
        {
            using var client = new HttpClient();
            var response = client.GetAsync($"https://api.nuget.org/v3-flatcontainer/{id}/index.json").GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var j = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var jo = JObject.Parse(j);
            var list = jo["versions"].Select(s => (string)s).ToList();
            list.Reverse();
            return list;
        }
    }
}
