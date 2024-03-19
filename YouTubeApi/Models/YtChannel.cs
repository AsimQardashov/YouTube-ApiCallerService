namespace YouTubeApi.Models;

public partial class YtChannel
{
    public int ChannelId { get; set; }

    public string ChannelUsername { get; set; } = null!;

    public string ChannelYtId { get; set; }

    public int? ChannelSubcribersCount { get; set; }

    public string ChannelTitle { get; set; }

    public int ChannelStatus { get; set; }

    public DateTime? ChannelCreatedAt { get; set; }

    public DateTime? ChannelUpdatedAt { get; set; }
}
