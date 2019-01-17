using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.Models
{
    public class ReturnDataOutput
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public string ResultId { get; set; }

        public object Data { get; set; }
    }
}
