/* Abstract Class defines Document Library Functions
 * 
 */

using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Generic
{
    public class SpproDocLibraryRepository<T> : SpproRepository<T> where T : ISpproEntity
    {

        #region Constructors

        public SpproDocLibraryRepository(string listName, ClientContext clientContext)
            : base(listName, clientContext)
        {
        }

        #endregion


    }
}
