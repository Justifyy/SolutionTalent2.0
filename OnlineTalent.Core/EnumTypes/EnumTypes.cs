using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.EnumTypes
{
    public class EnumTypes
    {
        public enum ErrorCode
        {
            Success = 1000,
            Error = 1001,
            Invalid = 1002
        };

        public enum ErrorMessages
        {
            Ok,
            Error,
            File_Is_Not_Exist,

        };

        public enum LogBeginPosition
        {
            Start,
            Middle,
            Finish
        }
    }
}
