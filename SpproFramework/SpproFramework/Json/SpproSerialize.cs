using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SpproFramework.Json
{
    public class SpproSerialize
    {
        /// <summary>
        /// Serialize Json as a Content Result.
        /// Fix's the NaN Conversation if returning a object that has a Geolocation field (Altitude is often NaN which can not be converted by Ajax)
        /// </summary>
        /// <param name="toConvert"></param>
        /// <returns></returns>
        public static ContentResult JsonReturn(object toConvert)
        {
            var settings = new JsonSerializerSettings();
            var floatConverter = new LawAbidingFloatConverter();
            settings.Converters.Add(floatConverter);
            var contentResult = new ContentResult();
            contentResult.Content = JsonConvert.SerializeObject(toConvert, settings);
            contentResult.ContentType = "application/json";
            return contentResult;
        }
    }
}
