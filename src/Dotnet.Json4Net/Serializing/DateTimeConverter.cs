using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dotnet.Serializing
{
    public class DateTimeConverter : IsoDateTimeConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

    }
}