using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events;
using Exiled.Loader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EXILEDPluginUpdater
{
    public class PluginUpdaterMain : Plugin<UpdaterConfig>
    {
        public override string Name => "PluginUpdater";
        public override string Author => "VirtualBrightPlayz";
        public override Version Version => new Version(1, 0, 0);
        public override PluginPriority Priority => PluginPriority.Last;

        public override void OnDisabled()
        {
            base.OnDisabled();
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Debug("Test\n" + JsonConvert.SerializeObject(new Version(1, 1, 1, 1)));
            Log.Info("Updating all plugins... (This may lag)");
            foreach (IPlugin<IConfig> plugin in Loader.Plugins)
            {
                if (Config.doNotUpdate.Contains(plugin.Name))
                {
                    Log.Info("Skipping update of " + plugin.Name);
                    return;
                }
                Type type = plugin.GetType();
                foreach (Type i in type.GetInterfaces())
                {
                    if (i == typeof(IAutoUpdate))
                    {
                        Log.Info("Downloading update manifest for " + plugin.Name);
                        PropertyInfo updateurl = null;
                        foreach (var prop in type.GetProperties())
                        {
                            if (prop.Name == "UpdateManifestUrl")
                            {
                                updateurl = prop;
                                break;
                            }
                        }
                        if (updateurl == null)
                            continue;
                        string url = (string)updateurl.GetValue(plugin);
                        try
                        {
                            string data = new WebClient().DownloadString(url);
                            ManifestConfig conf = JsonConvert.DeserializeObject<ManifestConfig>(data);
                            ManifestConfigVersion newversion = new ManifestConfigVersion()
                            {
                                VersionId = plugin.Version
                            };
                            foreach (ManifestConfigVersion version in conf.Versions)
                            {
                                if (version.VersionId > newversion.VersionId)
                                {
                                    newversion = version;
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(newversion.DllUrl) && newversion.VersionId != Version)
                            {
                                Log.Info("Downloading new update for " + plugin.Name);
                                try
                                {
                                    byte[] arr = new WebClient().DownloadData(newversion.DllUrl);
                                    File.WriteAllBytes(plugin.Assembly.Location, arr);
                                }
                                catch (WebException e)
                                {
                                    Log.Error("Error downloading new update for plugin " + plugin.Name + "\n" + e);
                                }
                                catch (IOException e)
                                {
                                    Log.Error("Error downloading new update for plugin " + plugin.Name + "\n" + e);
                                }
                            }
                        }
                        catch (WebException e)
                        {
                            Log.Error("Error downloading update manifest for plugin " + plugin.Name + "\n" + e);
                        }
                    }
                }
            }
        }
    }
}
