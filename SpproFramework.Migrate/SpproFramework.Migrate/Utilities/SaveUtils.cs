using Newtonsoft.Json;
using SpproFramework.Migrate.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Migrate.Utilities
{
    class SaveUtils
    {
        public void SaveInfo(SelectedLists saveInfo, string path)
        {
            var file = JsonConvert.SerializeObject(saveInfo);
            File.WriteAllText(path, file);
        }

        public SelectedLists OpenInfo(string path)
        {
            var jsonText = File.OpenText(path).ReadToEnd();
            return JsonConvert.DeserializeObject<SelectedLists>(jsonText);
        }
    }
}
