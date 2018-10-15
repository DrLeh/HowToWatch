using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HowToWatch.Models
{
    /// <summary>
    /// represents a response coming from the external service
    /// </summary>
    public class SourceResponse
    {
        public List<WatchItem> Items { get; set; }
    }

    public class WatchItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string FullPath { get; set; }
        public SourceFullPaths FullPaths { get; set; }
        public string Poster { get; set; }
        public string ShortDescription { get; set; }
        public long OriginalReleaseYear { get; set; }
        public double TmdbPopularity { get; set; }
        public string ObjectType { get; set; }
        public string OriginalTitle { get; set; }
        public List<Offer> Offers { get; set; }
        public List<SourceScoring> Scoring { get; set; }
        //
        //public Language OriginalLanguage { get; set; }
        public long? Runtime { get; set; }
        public string AgeCertification { get; set; }
        public long? MaxSeasonNumber { get; set; }
    }

    public class SourceFullPaths
    {
        public string MovieDetailOverview { get; set; }
        public string ShowDetailOverview { get; set; }
    }

    public class Offer
    {
        public string MonetizationType { get; set; }
        public long ProviderId { get; set; }
        public double? RetailPrice { get; set; }
        //
        //public Currency? Currency { get; set; }
        public SourceUrls Urls { get; set; }
        //
        //public Language[] SubtitleLanguages { get; set; }
        public object[] AudioLanguages { get; set; }
        public string PresentationType { get; set; }
        public string DateCreatedProviderId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        //
        //public Country Country { get; set; }
        public double? LastChangeRetailPrice { get; set; }
        public long? LastChangeDifference { get; set; }
        public double? LastChangePercent { get; set; }
        public DateTimeOffset? LastChangeDate { get; set; }
        public string LastChangeDateProviderId { get; set; }
        public string Type { get; set; }
        public long? ElementCount { get; set; }
        public long? NewElementCount { get; set; }
    }

    public class SourceUrls
    {
        public string StandardWeb { get; set; }
        public string DeeplinkIos { get; set; }
    }

    public class SourceScoring
    {
        public string ProviderType { get; set; }
        public double Value { get; set; }
    }
}
