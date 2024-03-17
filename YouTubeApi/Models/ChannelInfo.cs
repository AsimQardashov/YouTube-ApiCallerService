namespace YouTubeApi.Models
{
    public class ChannelInfo
    {
        public List<ChannelItem> Items { get; set; }
    }
    public class ChannelItem
    {
        public string Id { get; set; }
        public Snippet Snippet { get; set; }
        public Statistics Statistics { get; set; }
    }

    public class Snippet
    {
        public string Title { get; set; }
    }
    public class Statistics
    {
        public string ViewCount { get; set; }
        public int SubscriberCount { get; set; }
        public bool HiddenSubscriberCount { get; set; }
        public string VideoCount { get; set; }
    }
}
