using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Screenshot.Model;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace Screenshot.Common
{
    public class SerializeConfiguration
    {
        public static string pathArchive { get; private set; }
        private const string pathSystem = @"C:\\Windows\system32\";

        public static bool CreateXml(Configuration configuration)
        {
            try
            {
                var c = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                var path = Path.Combine(c, "ConfigurationScreenshotTeamviewer.xml");
                StreamWriter streamWriter = new StreamWriter(path);
                serializer.Serialize(streamWriter, configuration);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Create Xml: " + ex.Message);
            }
        }

        public static Configuration ReaderXml()
        {
            try
            {
                var c = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                var path = Path.Combine(c, "ConfigurationScreenshotTeamviewer.xml");
                StreamReader streamReader = new StreamReader(path);
                Configuration configuration = (Configuration)serializer.Deserialize(streamReader);
                return configuration;
            }
            catch (Exception ex)
            {

                throw new Exception("Reader Xml: " + ex.Message);
            }
        }

        public static bool ExistArchice()
        {
            try
            {
                var c = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
                var directory = Directory.GetCurrentDirectory();
                var path = Path.Combine(c, "ConfigurationScreenshotTeamviewer.xml");
                bool IsExist = File.Exists(path);
                int count = 0;

                while (!IsExist)
                {
                    ThreadManager.ThreadManagerApp(10000);

                    IsExist = File.Exists(path);

                    if (count >= 5)
                        break;

                    count++;
                }
                pathArchive = path;

                return File.Exists(pathArchive);
            }
            catch (Exception ex)
            {

                throw new Exception("Exist Archice: " + ex.Message);
            }
        }
    }
}
