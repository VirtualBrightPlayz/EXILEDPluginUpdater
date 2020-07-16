using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace EXILEDPluginUpdater
{
    public class UpdaterConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public List<string> doNotUpdate { get; set; } = new List<string>();
    }
}