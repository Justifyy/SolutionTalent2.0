using System;

namespace OnlineTalent.Core.Data
{
    /// <summary> The base 2 entity. </summary>
    public class Base2Entity : BaseEntity {
        /// <summary> Gets or sets the display order. </summary>
        public bool DisplayOrder { get; set; }

        /// <summary> Gets or sets the start date. </summary>
        public DateTime? StartDate { get; set; }

        /// <summary> Gets or sets the end date. </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Gets or sets the status. </summary>
        public int? StatusId { get; set; }
    }
}
