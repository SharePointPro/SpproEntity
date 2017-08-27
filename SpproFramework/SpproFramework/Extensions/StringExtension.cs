using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        ///  Convert SharePoint Field Name to valid C# Property Name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string CleanName(this string propertyName)
        {
            var cleanName = propertyName.Replace("_x0020_", "").Replace(" ", "").Replace("\\", "");
            foreach (var c in System.IO.Path.GetInvalidFileNameChars())
            {
                cleanName = cleanName.Replace(c.ToString(), "");
            }
            cleanName = cleanName.Replace("(", "").Replace(")", "");
            cleanName = cleanName.Replace("%", "Percent");
            cleanName = cleanName.Replace("-", "");
            return cleanName;
        }

        /// <summary>
        /// Converts Field Name to SharePoint Friendly Name (ie replaces _x0020_)
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string DirtyName(this string propertyName)
        {
            return propertyName.AddSpacesToSentence(true).Replace(" ", "_x0020_");
        }

        public static string AddSpacesToSentence(this string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static SecureString ToSecureString(this string password)
        {
            var securePassword = new SecureString();
            //Convert string to secure string  
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }

        public static string TrimLastCharacter(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            else
            {
                return str.TrimEnd(str[str.Length - 1]);
            }
        }
    }
}
