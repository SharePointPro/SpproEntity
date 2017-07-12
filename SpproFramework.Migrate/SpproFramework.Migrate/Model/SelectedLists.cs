using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Migrate.Model
{
    internal class SelectedList
    {
        [JsonIgnoreAttribute]
        public List List { get; set; }

        private Guid _ListGuid { get; set; }
     
        public Guid ListGuid
        {
            get
            {
                if (List == null)
                    return _ListGuid;
                else
                    return List.Id;
            }
            set
            {
                this._ListGuid = value;
            }

        }


        [JsonIgnoreAttribute]
        public List<Field> CheckedField { get; set; }

        [JsonIgnoreAttribute]
        private List<Guid> _CheckedFieldGuids { get; set; }

        public List<Guid> CheckedFieldGuids
        {
            get
            {
                if (CheckedField != null && CheckedField.Count > 0)
                {
                    return CheckedField.Select(a => a.Id).ToList();
                }
                else
                {
                    return _CheckedFieldGuids;
                }
            }
            set
            {
                _CheckedFieldGuids = value;
            }
        }
    }
    class SelectedLists
    {
        public string NamespaceString { get; set; }

        public string ModelFoler { get; set; }

        public string ContextName { get; set; }

        public List<SelectedList> SelectedListCollection { get; set; }
    }
}
