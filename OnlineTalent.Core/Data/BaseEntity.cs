using System;

namespace OnlineTalent.Core.Data
{
    public class BaseEntity: IBaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }

        /// <summary> Gets or sets a value indicating whether is active. </summary>
        public bool IsActive { get; set; }

        /// <summary> Gets or sets a value indicating whether is deleted. </summary>
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
