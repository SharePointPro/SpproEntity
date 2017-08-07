﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Json
{
    public class LawAbidingFloatConverter : JsonConverter
    {
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var val = value as double? ?? (double?)(value as float?);
            if (val == null || Double.IsNaN((double)val) || Double.IsInfinity((double)val))
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue((double)val);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(float);
        }
    }

}
