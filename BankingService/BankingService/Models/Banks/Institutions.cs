using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BankingService.Models.Banks
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Institutions
    {
        USAA
    }
}