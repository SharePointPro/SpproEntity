using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Attributes
{
    public class SpproListAttribute : Attribute
    {
        public string ListName { get; set; }
        public bool Recursive { get; set; }
    }

}
