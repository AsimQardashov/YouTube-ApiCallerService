namespace YouTubeApi.Models;

public partial class YtChannelNews
{
    public int NewsId { get; set; }

    public string NewsTitle { get; set; } = null!;

    public string NewsYtId { get; set; }

    public string NewsUrl { get; set; } = null!;

    public DateTime NewsPublishDate { get; set; }

    public DateTime? NewsAddDate { get; set; }

    public int? NewsViewCount { get; set; }

    public int? NewsLikeCount { get; set; }

    public int ChannelId { get; set; }

    public virtual YtChannel Channel { get; set; } = null!;
}
