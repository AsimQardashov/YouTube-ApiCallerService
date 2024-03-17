using Newtonsoft.Json;
using YouTubeApi.Interfaces;
using YouTubeApi.Models;

namespace YouTubeApi.Concrete
{

    public class ChannelConcrete : IChannelInterface
    {
        private readonly DBContext db;
        public ChannelConcrete()
        {
            db = new();
        }

        public async Task<string> CreateChannel(string channelUserName)
        {
            try
            {
                ChannelInfo channelInfo = await SearchChannelDetail(channelUserName);
                YtChannel newChannel = new()
                {
                    ChannelUsername = channelUserName,
                    ChannelTitle = channelInfo.Items[0].Snippet.Title,
                    ChannelYtId = channelInfo.Items[0].Id,
                    ChannelSubcribersCount = channelInfo.Items[0].Statistics.SubscriberCount,
                };
                db.YtChannels.Add(newChannel);
                db.SaveChanges();
                return newChannel.ChannelYtId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateChannel method error.. " + ex.InnerException);
                throw;
            }
        }

        public async Task<List<string>> GetChannelsYtIds()
        {
            try
            {
                List<string> ChannelsYtIds = db.YtChannels.Where(x => x.ChannelStatus != 0).Select(x => x.ChannelYtId).ToList();
                return ChannelsYtIds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ChannelInfo> SearchChannelDetail(string channelUserName)
        {
            try
            {
                string searchUrl = $"https://www.googleapis.com/youtube/v3/channels?part=id%2Csnippet%2Cstatistics&forUsername={channelUserName}&key=YOUR_API_KEY";
                HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(searchUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    ChannelInfo channelInfo = JsonConvert.DeserializeObject<ChannelInfo>(jsonContent);
                    return channelInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SearchChannelDetail method error.. " + ex.InnerException);
                throw;
            }
        }

        public async Task<List<string>> SearchVideos(string channelYtId)
        {
            try
            {
                string searchUrl = $"https://www.googleapis.com/youtube/v3/search?part=snippet&channelId={channelYtId}&maxResults=2&order=date&key=YOUR_API_KEY";
                HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(searchUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(jsonContent);
                    List<string> videoIds = searchResult.Items.Select(item => item.Id.VideoId).ToList();
                    return videoIds;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SearchVideos method error.. " + ex.InnerException);
                throw;
            }
        }
    }
}
