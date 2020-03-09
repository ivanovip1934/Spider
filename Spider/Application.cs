using System;

namespace Spider
{
    [Serializable]
    public class App
    {
        public string DisplayName { get; set; }
        public string DisplayVersion { get; set; }
        public string UninstallString { get; set; }
        public string InstallLocation { get; set; }
        public string Publisher { get; set; }
        public string PathInRegistry { get; set; }

        public App() { }
        public App(string displayName, string displayVersion, string uninstallString, string installLocation, string publisher, string pathinregistry)
        {
            DisplayName = displayName;
            DisplayVersion = displayVersion;
            UninstallString = uninstallString;
            InstallLocation = installLocation;
            Publisher = publisher;
            PathInRegistry = pathinregistry;
        }

        public bool IsSame(App app) {
            if (this.DisplayName == app.DisplayName
                    && this.DisplayVersion == app.DisplayVersion
                    && this.UninstallString == app.UninstallString
                    && this.InstallLocation == app.InstallLocation
                    && this.Publisher == app.Publisher
                    && this.PathInRegistry == app.PathInRegistry)
                return true;
            else
                return false;
        }
    }

    [Serializable]
    public class Appv2 
    { 
        public int Count { get; set; }
        public App AppItem { get; set; }
        public Appv2() { }

        public Appv2(App appitem, int count)
        {
            this.AppItem = appitem;
            this.Count = count;
        }
    }
}

