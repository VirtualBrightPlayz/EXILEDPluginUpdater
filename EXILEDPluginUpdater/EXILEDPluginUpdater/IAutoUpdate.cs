using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXILEDPluginUpdater
{
    public interface IAutoUpdate
    {
        string UpdateManifestUrl { get; }
    }
}
