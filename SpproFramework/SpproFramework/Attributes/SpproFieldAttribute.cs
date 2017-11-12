using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Attributes
{
    public class SpproFieldAttribute : Attribute
    {
        /// <summary>
        /// Field Types, example Lookup, User, Counter
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// For Lookup or User, can be Id, Value, IdAndValue
        /// Id must be List of int
        /// Value must be List of string
        /// IdAndValue must be List of Dynamics
        /// </summary>
        public string FieldValue { get; set; }

        public string SpName { get; set; }

        public bool ReadOnly { get; set; }

        public string DisplayName { get; set; }
    }

    public class SpproNavigationAttribute : Attribute
    {
        public bool NavigationProperty { get; set; }

        public bool ForeignKey { get; set; }

        /// <summary>
        /// The Foreign key name in foreign list
        /// </summary>
        public string LookupField { get; set; }
    }
}
