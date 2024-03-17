using YouTubeApi.Models;

namespace YouTubeApi.Interfaces
{
    public interface IChannelInterface
    {
        public Task<List<string>> SearchVideos(string channelYtId);
        public Task<List<string>> GetChannelsYtIds();
        public Task<ChannelInfo> SearchChannelDetail(string channelUserName);
        public Task<string> CreateChannel(string channelUserName);
    }

}
