using System.Text.Json.Serialization;

namespace Application.JSONResponseModel.NumLookUpService;

public class NumberLookUpResponseModel
{
    [JsonPropertyName("valid")]
        public bool Valid { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("local_format")]
        public string LocalFormat { get; set; }

        [JsonPropertyName("international_format")]
        public string InternationalFormat { get; set; }

        [JsonPropertyName("country_prefix")]
        public string CountryPrefix { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("carrier")]
        public string Carrier { get; set; }

        [JsonPropertyName("line_type")]
        public string LineType { get; set; }
}
