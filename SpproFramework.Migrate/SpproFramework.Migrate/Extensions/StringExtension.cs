using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Migrate.Extensions
{
    internal static class StringExtension
    {
        public static SecureString ToSecureString(this string password)
        {
            var securePassword = new SecureString();
            //Convert string to secure string  
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }
    }
}
