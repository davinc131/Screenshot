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

                string path = conf.PathProgram;

                Process.Start(path);
            }
            catch (Exception ex)
            {

                throw new Exception("Start Program: " + ex.Message);
            }
        }
    }
}
