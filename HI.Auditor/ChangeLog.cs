using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HI.Auditor
{
    /// <summary>
    /// This class contains instances of changes in the classes
    /// </summary>
    public class ChangeLog
    {
        #region Properties
        /// <summary>
        /// The main oject type source that is being checked
        /// </summary>
        public string ObjectType { get; set; }
        /// <summary>
        /// The main oject name source
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// An identifier for logging changes to a unique object id
        /// </summary>
        public Guid ObjectId { get; set; }
        /// <summary>
        /// The property name. Where it is a deep property, the parent and child property is logged
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// The original value of the property
        /// </summary>
        public string ValueOld { get; set; }
        /// <summary>
        /// The changed value of the property
        /// </summary>
        public string ValueNew { get; set; }
        /// <summary>
        /// The guid value of the property
        /// </summary>
        public Guid ValueGuid { get; set; }
        /// <summary>
        /// The guid value of the property
        /// </summary>
        public string ValueDocKey { get; set; }
        public int ValueChangeType { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of the change class
        /// </summary>
        /// <param name="objectType">The main oject type source that is being checked</param>
        /// <param name="objectId">An identifier for logging changes to a unique object id</param>
        /// <param name="property">The property name. Where it is a deep property, the parent and child property is logged</param>
        /// <param name="oldValue">The original value of the property</param>
        /// <param name="newValue">The changed value of the property</param>
        /// <param name="orgGuid">The get original guid value of the property</param>
        public ChangeLog(string objectName, Guid objectId, string property, string oldValue, string newValue, Guid orgGuid, string refDocKey, int ChangeType)
        {
            //this.ObjectType = objectType;
            this.ObjectName = objectName;
            this.ObjectId = objectId;
            this.Property = property;
            this.ValueOld = oldValue;
            this.ValueNew = newValue;
            this.ValueGuid = orgGuid;
            this.ValueDocKey = refDocKey;
            this.ValueChangeType = ChangeType;
        }
        #endregion
    }
}
