using YouTubeApi.Concrete;
using YouTubeApi.Models;

namespace YouTubeApi
{
    public class MainProgram
    {
        private readonly ChannelConcrete _channelConcrete;
        private readonly VideoConcrete _videoConcrete;

        public MainProgram()
        {
            _channelConcrete = new ChannelConcrete();
            _videoConcrete = new VideoConcrete();
        }

        public async Task ApiCaller()
        {
            List<string> channelIds = await _channelConcrete.GetChannelsYtIds();

            foreach (var channelId in channelIds)
            {
                List<string> videoIds = await _channelConcrete.SearchVideos(channelId);
                if (videoIds == null)
                {
                    continue;
                }
                foreach (var videoId in videoIds)
                {
                    VideoInfo video = await _videoConcrete.GetVideoDetail(videoId);
                    if (video == null)
                    {
                        continue;
                    }
                    await _videoConcrete.SaveVideoDetail(video);
                    Console.WriteLine(video.Snippet.Title);
                }
            }
        }
    }
}
