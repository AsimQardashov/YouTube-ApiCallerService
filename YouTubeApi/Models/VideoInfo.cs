namespace YouTubeApi.Models
{
    public class YouTubeResponse
    {
        public string? Kind { get; set; }
        public List<VideoInfo>? Items { get; set; }
    }
    public class VideoInfo
    {
        public class SnippetInfo
        {
            public string Title { get; set; }
            public string PublishedAt { get; set; }
        }

        public string Id { get; set; }
        public SnippetInfo Snippet { get; set; }

        public class StatisticsInfo
        {
            public int ViewCount { get; set; }
            public int LikeCount { get; set; }
        }
        public StatisticsInfo Statistics { get; set; }
    }
}

