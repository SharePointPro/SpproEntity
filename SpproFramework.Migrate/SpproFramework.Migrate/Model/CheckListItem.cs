using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Migrate.Model
{
    internal class CheckListItem
    {
        public string Display { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            return Display;
        }
    }
}
