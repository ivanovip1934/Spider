using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Spider
{
    public class ShowResultSearch
    {
        const string PathToDir = @"\\fileserv.omsu.vmr\Inventory$\SearchApps";
        public string CurrentDirectory { get; private set; }
        public string CurrentFile { get; private set; }
        public SortedDictionary<string, List<App>> DicPCApps{ get { return GetPCApps(); } private set { } }


        public string[] GetNameDirectories()
        {
            string[] directories = Directory.GetDirectories(PathToDir);            
            string[] nameDir = new string[directories.Length];
            for (int i = 0; i < directories.Length; i++)
            {
                nameDir[i] = (new DirectoryInfo(directories[i])).Name;
            }
            return nameDir;
        }

        public void SetNameCurDir(string nameDir)
        {
            CurrentDirectory = Path.Combine(PathToDir, nameDir);
        }

        public string[] GetNamePC()
        {
            string[] listFiles = Directory.GetFiles(CurrentDirectory);
            string[] namePC = new string[listFiles.Length];
            for (int i = 0; i < listFiles.Length; i++)
            {
                namePC[i] = (new FileInfo(listFiles[i])).Name.Replace(".xml", "");
            }
            return namePC;
        }

        public void SetNameCurFile(string namePC) {
            CurrentFile = Path.Combine(CurrentDirectory, $"{namePC}.xml");
        }

        private SortedDictionary<string,List<App>> GetPCApps()
        {
            SortedDictionary<string, List<App>>  dicPCApps = new SortedDictionary<string, List<App>>();
            FileInfo finfo;
            string NamePC = String.Empty;
            List<App> appsfromxml;
            foreach (string xmlfile in Directory.GetFiles(CurrentDirectory))
            {
                finfo = new FileInfo(xmlfile);
                NamePC = finfo.Name.Replace(finfo.Extension, "");
                appsfromxml = ReadFromXmlFile<List<App>>(xmlfile);
                dicPCApps.Add(NamePC, appsfromxml);
            }
            return dicPCApps;
        }

        public List<Appv2> GetUnicVersion() {
            var Apps = (from item in DicPCApps
                         let lsapps = item.Value
                         from app in lsapps
                         group app by new { app.DisplayName, app.DisplayVersion, app.UninstallString, app.InstallLocation,app.Publisher,app.PathInRegistry }
                         into g
                         select new Appv2(new App(g.Key.DisplayName, g.Key.DisplayVersion, g.Key.UninstallString, g.Key.InstallLocation, g.Key.Publisher, g.Key.PathInRegistry), g.Count())).ToList();
            //Dictionary<int, Appv2> dic = Apps.Select((s, i) => new { s, i }).ToDictionary(x => x.i, x => x.s);            
            return Apps;
            
        }

        private static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                try
                {
                    var serializer = new XmlSerializer(typeof(T));
                    reader = new StreamReader(stream);
                    return (T)serializer.Deserialize(reader);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
        }
    }
}
