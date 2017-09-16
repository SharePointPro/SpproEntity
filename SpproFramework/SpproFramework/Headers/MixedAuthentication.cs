using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using SpproFramework.Extensions;
using SpproFramework.Attributes;
using Microsoft.SharePoint.Client;
using System.IO;

namespace SpproFramework.Headers
{
    public class MixedAuthentication
    {
        #region Private Methods

        //Event Handler
        public static void ctx_MixedAuthRequest(object sender, WebRequestEventArgs e)
        {
            try
            {
                //Add the header that tells SharePoint to use Windows authentication.
                e.WebRequestExecutor.RequestHeaders.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

    }
}
