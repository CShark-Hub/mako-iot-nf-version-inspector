using Newtonsoft.Json;

namespace Mako.IoT.NFVersionInspector
{
    public class Storage
    {
        public static IEnumerable<Package> Load(string id)
        {
            return LoadFromFile(GetFilePath($"{id}.nugetcache")) ?? Array.Empty<Package>();
        }

        public static void Save(string id, IEnumerable<Package> packages)
        {
            SaveToFile(GetFilePath($"{id}.nugetcache"), packages);
        }

        public static void SaveBoardInfo(string name, IEnumerable<Package> packages)
        {
            SaveToFile(GetFilePath($"{name}.boardcache"), packages);
        }

        public static IEnumerable<Package> LoadBoardInfo(string name)
        {
            return LoadFromFile(GetFilePath($"{name}.boardcache")) ?? throw new FileNotFoundException();
        }

        public static IEnumerable<string> ListBoardInfo()
        {
            return Directory.GetFiles(GetFilePath(String.Empty), "*.boardcache")
                .Select(Path.GetFileNameWithoutExtension)!;
        }

        public static IEnumerable<Package>? LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            try
            {
                using var reader = new StreamReader(fileName);
                var s = reader.ReadToEnd();
                reader.Close();
                return JsonConvert.DeserializeObject<IEnumerable<Package>>(s);
            }
            catch (Exception e)
            {

            }

            return null;
        }

        private static void SaveToFile(string fileName, IEnumerable<Package> packages)
        {
            var s = JsonConvert.SerializeObject(packages);
            if (File.Exists(fileName))
                File.Delete(fileName);
            using var sw = new StreamWriter(fileName);
            sw.Write(s);
            sw.Close();
        }

        private static string GetFilePath(string fileName)
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Mako.IoT.NFVersionInspector");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return fileName == String.Empty ? folder : Path.Combine(folder, fileName);
        }

        public static void ClearBoardsInfo()
        {
            foreach (var file in Directory.GetFiles(GetFilePath(String.Empty), "*.boardcache"))
            {
                File.Delete(file);
            }
        }

        public static void ClearPackages()
        {
            foreach (var file in Directory.GetFiles(GetFilePath(String.Empty), "*.nugetcache"))
            {
                File.Delete(file);
            }
        }
    }
}
