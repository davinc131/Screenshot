using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Screenshot.Common
{
    public class StartProgram
    {
        public StartProgram()
        {
            Init();
        }

        private void Init()
        {
            try
            {
                var conf = SerializeConfiguration.ReaderXml();

                ProcessStartInfo startInfo = new ProcessStartInfo(conf.PathProgram);
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;

                string path = conf.PathProgram;

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {

                throw new Exception("Start Program: " + ex.Message);
            }
        }
    }
}
