using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.Data
{
    public interface IBaseEntity
    {
        /// <summary> Gets or sets the ıd. </summary>
        int Id { get; set; }

        /// <summary> Gets or sets a value indicating whether ıs active. </summary>
        bool IsActive { get; set; }

        /// <summary> Gets or sets a value indicating whether ıs deleted. </summary>
        bool IsDeleted { get; set; }

        /// <summary> Gets or sets the created user ıd. </summary>
        int? CreatedBy { get; set; }

        /// <summary> Gets or sets the created date time. </summary>
        DateTime CreatedDate { get; set; }

        /// <summary> Gets or sets the updated user ıd. </summary>
        int? ModifyBy { get; set; }

        /// <summary> Gets or sets the updated date time. </summary>
        DateTime? ModifyDate { get; set; }
    }
}
