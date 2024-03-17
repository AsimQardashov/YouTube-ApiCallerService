using Newtonsoft.Json;
using YouTubeApi.Interfaces;
using YouTubeApi.Models;

namespace YouTubeApi.Concrete
{
    public class VideoConcrete : IVideoInterface
    {
        private readonly DBContext db;

        public VideoConcrete()
        {
            db = new();
        }

        public async Task<VideoInfo> GetVideoDetail(string videoId)
        {
            string url = $"https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&id={videoId}&key=account-key";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonContent = await response.Content.ReadAsStringAsync();

                YouTubeResponse? youtubeResponse = JsonConvert.DeserializeObject<YouTubeResponse>(jsonContent);

                return youtubeResponse.Items[0];
            }
        }

        public async Task SaveVideoDetail(VideoInfo videoInfo)
        {
            YtChannelNews newChannelNews = new()
            {
                NewsTitle = videoInfo.Snippet.Title,
                NewsPublishDate = DateTime.Parse(videoInfo.Snippet.PublishedAt),
                NewsViewCount = videoInfo.Statistics.ViewCount,
                NewsLikeCount = videoInfo.Statistics.LikeCount,
            };
            db.YtChannelNews.Add(newChannelNews);
            await db.SaveChangesAsync();
        }
    }
}
