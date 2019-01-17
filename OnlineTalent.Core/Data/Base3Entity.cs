using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.Data
{
    public class Base3Entity : BaseEntity
    {
        /// <summary> Gets or sets the start date. </summary>
        public DateTime? StartDate { get; set; }

        /// <summary> Gets or sets the end date. </summary>
        public DateTime? EndDate { get; set; }

    }
}
