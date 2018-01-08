using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            _createDate = DateTime.Now;
            IsDeleted = false;
        }

        public abstract long Key();

        /// <summary>
        /// Field to indicate entity status for Soft Delete.
        /// If the value is true the entity is available for use in the application otherwise entity is deleted.
        /// </summary>        
        public bool IsDeleted { get; set; }

        private DateTime _createDate;
        /// <summary>
        /// Creation date and time of the eitity.
        /// </summary>        
        public DateTime CreateDate => _createDate;//{ get; }
    }
}
