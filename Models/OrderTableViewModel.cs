using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Play2Money.Models {
    public class OrderTableViewModel {
        public int Id { get; set; }
        
        [Url]
        public string Reference { get; set; }
        public bool IsDone { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy H:mm:ss")]
        public DateTime Created { get; set; }

        public string PlayerGlobalUid {get; set;}
    }

    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}