/* Abstract Class defines Document Library Functions
 * 
 */

using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region Private Methods

        private Folder CreateFolderInternal(Folder parentFolder, string fullFolderUrl)
        {
            var folderUrls = fullFolderUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string folderUrl = folderUrls[0];
            var curFolder = parentFolder.Folders.Add(folderUrl);
            ClientContext.Load(curFolder);
            ClientContext.ExecuteQuery();
            if (folderUrls.Length > 1)
            {
                var subFolderUrl = string.Join("/", folderUrls, 1, folderUrls.Length - 1);
                return CreateFolderInternal(curFolder, subFolderUrl);
            }
            return curFolder;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create Folder client object
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listTitle"></param>
        /// <param name="fullFolderUrl"></param>
        /// <returns></returns>
        public Folder CreateFolder(string fullFolderUrl)
        {
            if (string.IsNullOrEmpty(fullFolderUrl))
                throw new ArgumentNullException("fullFolderUrl");
            var list = GetList();
            return CreateFolderInternal(list.RootFolder, fullFolderUrl);
        }

        /// <summary>
        /// Search Document Library for file based on filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByFileName(string fileName)
        {
            var list = GetList();
            var itemList = new List<T>();
            CamlQuery query = new CamlQuery();
            query.ViewXml = string.Format("<View Scope=\"RecursiveAll\"><Query><Where><Eq><FieldRef Name='FileLeafRef' /><Value Type='File'>{0}</Value></Eq></Where></Query></View>", fileName);
            var listItems = list.GetItems(query);
            ClientContext.Load(listItems);
            ClientContext.ExecuteQuery();
            foreach (var item in listItems)
            {
                itemList.Add(await PopulateSEntity(item));
            }
            return itemList;
        }

        #endregion

    }
}
