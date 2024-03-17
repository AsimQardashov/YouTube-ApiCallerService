using YouTubeApi.Models;

namespace YouTubeApi.Interfaces
{
    public interface IVideoInterface
    {
        public Task<VideoInfo> GetVideoDetail(string videoId);
        public Task SaveVideoDetail(VideoInfo videoInfo);
    }
}
