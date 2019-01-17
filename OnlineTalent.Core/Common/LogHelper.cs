using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.Common
{
    public class LogHelper
    {
        public static void WriteLog(string functName,string mesaj)
        {
            //HelperText.LogPath = 
            HelperText.CreateTextFile(functName, mesaj);

        }
    }
}
