using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTalent.Core.Models
{
    public class ReturnOutput
    {
        public ReturnOutput()
        {
            ErrorCode = EnumTypes.EnumTypes.ErrorCode.Success.ToString();
            ErrorMessage = EnumTypes.EnumTypes.ErrorMessages.Ok.ToString();
        }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}