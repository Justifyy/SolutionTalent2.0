using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.Common
{
    public class ReturnErrorCodes
    {
        public enum ErrorCode
        {
            Success = 1000,
            Error = 1001,
            Invalid = 1002
        };

        enum ErrorMessages
        {
            File_Is_Not_Exist
        };
    }
}
