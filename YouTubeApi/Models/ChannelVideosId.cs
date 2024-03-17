namespace YouTubeApi.Models
{
    public class SearchResult
    {
        public List<VideoResult> Items { get; set; }
    }

    public class VideoResult
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public Id Id { get; set; }
    }

    public class Id
    {
        public string Kind { get; set; }
        public string VideoId { get; set; }
    }
}
