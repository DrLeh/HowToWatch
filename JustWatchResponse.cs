using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace HowToWatch.JustWatch
{
    //quicktype.io
    public class JustWatchResponse
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("page_size")]
        public long PageSize { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }

        [JsonProperty("items")]
        public JustWatchItem[] Items { get; set; }
    }

    public class JustWatchItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("full_path")]
        public string FullPath { get; set; }

        [JsonProperty("full_paths")]
        public JustWatchFullPaths FullPaths { get; set; }

        [JsonProperty("poster")]
        public string Poster { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("original_release_year")]
        public long OriginalReleaseYear { get; set; }

        [JsonProperty("tmdb_popularity")]
        public double TmdbPopularity { get; set; }

        [JsonProperty("object_type")]
        public string ObjectType { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("offers")]
        public JustWatchOffer[] Offers { get; set; }

        [JsonProperty("scoring")]
        public JustWatchScoring[] Scoring { get; set; }

        //[JsonProperty("original_language")]
        //public Language OriginalLanguage { get; set; }

        [JsonProperty("runtime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Runtime { get; set; }

        [JsonProperty("age_certification", NullValueHandling = NullValueHandling.Ignore)]
        public string AgeCertification { get; set; }

        [JsonProperty("max_season_number", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxSeasonNumber { get; set; }
    }

    public class JustWatchFullPaths
    {
        [JsonProperty("MOVIE_DETAIL_OVERVIEW", NullValueHandling = NullValueHandling.Ignore)]
        public string MovieDetailOverview { get; set; }

        [JsonProperty("SHOW_DETAIL_OVERVIEW", NullValueHandling = NullValueHandling.Ignore)]
        public string ShowDetailOverview { get; set; }
    }

    public class JustWatchOffer
    {
        [JsonProperty("monetization_type")]
        public string MonetizationType { get; set; }

        [JsonProperty("provider_id")]
        public long ProviderId { get; set; }

        [JsonProperty("retail_price", NullValueHandling = NullValueHandling.Ignore)]
        public double? RetailPrice { get; set; }

        //[JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        //public Currency? Currency { get; set; }

        [JsonProperty("urls")]
        public JustWatchUrls Urls { get; set; }

        //[JsonProperty("subtitle_languages")]
        //public Language[] SubtitleLanguages { get; set; }

        [JsonProperty("audio_languages")]
        public object[] AudioLanguages { get; set; }

        [JsonProperty("presentation_type")]
        public string PresentationType { get; set; }

        [JsonProperty("date_created_provider_id")]
        public string DateCreatedProviderId { get; set; }

        [JsonProperty("date_created")]
        public DateTimeOffset DateCreated { get; set; }

        //[JsonProperty("country")]
        //public Country Country { get; set; }

        [JsonProperty("last_change_retail_price", NullValueHandling = NullValueHandling.Ignore)]
        public double? LastChangeRetailPrice { get; set; }

        [JsonProperty("last_change_difference", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastChangeDifference { get; set; }

        [JsonProperty("last_change_percent", NullValueHandling = NullValueHandling.Ignore)]
        public double? LastChangePercent { get; set; }

        [JsonProperty("last_change_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastChangeDate { get; set; }

        [JsonProperty("last_change_date_provider_id", NullValueHandling = NullValueHandling.Ignore)]
        public string LastChangeDateProviderId { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("element_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? ElementCount { get; set; }

        [JsonProperty("new_element_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? NewElementCount { get; set; }
    }

    public class JustWatchUrls
    {
        [JsonProperty("standard_web")]
        public Uri StandardWeb { get; set; }

        [JsonProperty("deeplink_ios", NullValueHandling = NullValueHandling.Ignore)]
        public string DeeplinkIos { get; set; }
    }

    public class JustWatchScoring
    {
        [JsonProperty("provider_type")]
        public string ProviderType { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    internal static class JustWatchConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                //CountryConverter.Singleton,
                //CurrencyConverter.Singleton,
                //MonetizationTypeConverter.Singleton,
                //PresentationTypeConverter.Singleton,
                //LanguageConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}