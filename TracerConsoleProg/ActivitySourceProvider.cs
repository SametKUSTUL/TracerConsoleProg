using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerConsoleProg
{
    public static class ActivitySourceProvider
    {
        public static ActivitySource Source = new ActivitySource("EmailSenderActivitySource");

        public static ActivitySource Source2 = new ActivitySource("EmailSenderActivitySourceToWriteFile");
    }
}
