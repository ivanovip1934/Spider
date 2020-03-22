using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Spider
{
    public class HWInfoGeneral        
    {

        
        string GetPathToDirARM()
        {
            if (Environment.MachineName == "DESKTOP-JO3F2UG")
                return @"P:\Progects\Spider\ARM";
            return @"\\fileserv.omsu.vmr\inventory$\ARM";
        }

        public SortedDictionary<string, ComputerInfo> GetDicPC()
        {            
            SortedDictionary<string, ComputerInfo> dicPC = new SortedDictionary<string, ComputerInfo>();
            //FileInfo finfo;
            //string NamePC = String.Empty;
            ComputerInfo pcFromXml;
            foreach (string xmlfile in Directory.GetFiles(GetPathToDirARM()))
            {
                //finfo = new FileInfo(xmlfile);
                //NamePC = finfo.Name.Replace(finfo.Extension, "");
                pcFromXml = ReadFromXmlFile<ComputerInfo>(xmlfile);
                dicPC.Add( pcFromXml.Name,pcFromXml);
            }
            return dicPC;
        }

        private static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(T));
                    reader = new StreamReader(stream);
                    return (T)serializer.Deserialize(reader);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"{e.Message}");
                    MessageBox.Show($"{filePath}");
                    return default(T);
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
