using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Screenshot.Common
{
    public class ThreadManager
    {
        public static void ThreadManagerApp(int value)
        {
            Thread.Sleep(value);
        }
    }
}
