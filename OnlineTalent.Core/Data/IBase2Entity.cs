using System;

namespace OnlineTalent.Core.Data
{
    /// <summary>
    /// The base 2 entity.
    /// </summary>
    public interface IBase2Entity : IBaseEntity {
        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        DateTime? EndDate { get; set; }
        
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        int StatusId { get; set; }
    }
}
