using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXILEDPluginUpdater
{
    public class ManifestConfig
    {
        public List<ManifestConfigVersion> Versions { get; set; } = new List<ManifestConfigVersion>();
    }

    public class ManifestConfigVersion
    {
        public string DllUrl { get; set; }
        public Version VersionId { get; set; }
    }
}
