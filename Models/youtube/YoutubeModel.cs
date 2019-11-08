using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Dashboard.Models.youtube
{
    public class YoutubeModel : IYoutubeModel
    {
        const string MYYOUTUBE_CHANNEL = "SampleChannel";
        const string MyYOUTUBE_DEVELOPER_KEY = "AIzaSyC3aU9QVuq8vtkYlj0cJzK0CyL8vZ5d_78";
        private static string[] validAuthorities = { "youtube.com", "www.youtube.com", "youtu.be", "www.youtu.be" };

        private bool ValidUrl(string url)
        {
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);
            if (!validAuthorities.Contains(uri.Host))
                return false;
            return true;
        }

        private string GetIdVideo(string url)
        {
            if (!ValidUrl(url))
                return null;
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);
            if (query.AllKeys.Contains("v"))
            {
                return query["v"];
            }
            return uri.Segments.Last();
        }

        public VideoYoutube GetVideo(string url)
        {
            VideoYoutube toReturn = new VideoYoutube { dislikes = 0, likes = 0, Title = "", VideoUrl = "", viewCount = 0 };
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = MyYOUTUBE_DEVELOPER_KEY });
            string id = GetIdVideo(url);

            if (id == null)
            {
                return toReturn;
            }

            var videoStats = yt.Videos.List("statistics");
            videoStats.Id = id;
            var videoStatsResponse = videoStats.Execute();
            if (videoStatsResponse.Items.Count == 1)
            {
                toReturn.Title = "";
                toReturn.VideoUrl = "https://www.youtube.com/embed/" + id.ToString();
                toReturn.likes = videoStatsResponse.Items[0].Statistics.LikeCount;
                toReturn.dislikes = videoStatsResponse.Items[0].Statistics.DislikeCount;
                toReturn.viewCount = videoStatsResponse.Items[0].Statistics.ViewCount;
            } else {
                return toReturn;
            }

            var videoContent = yt.Videos.List("snippet");
            videoContent.Id = id;
            var videoContentResponse = videoContent.Execute();
            if (videoContentResponse.Items.Count == 1)
            {
                toReturn.Title = videoContentResponse.Items[0].Snippet.Title;
            }
            return toReturn;
        }

    public  List<VideoYoutube> GetChannelVideo()
        {
            List<VideoYoutube> toReturn = new List<VideoYoutube>();

            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = MyYOUTUBE_DEVELOPER_KEY });
            ChannelsResource.ListRequest channelsListRequest = yt.Channels.List("contentDetails");
            channelsListRequest.ForUsername = "aMOODIEsqueezie";
            var channelsListResponse = channelsListRequest.Execute();
            foreach (var channel in channelsListResponse.Items)
            {
                // of videos uploaded to the authenticated user's channel.
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = uploadsListId;
                playlistItemsListRequest.MaxResults = 50;
                // Retrieve the list of videos uploaded to the authenticated user's channel.
                var playlistItemsListResponse = playlistItemsListRequest.Execute();
                foreach (var playlistItem in playlistItemsListResponse.Items)
                {
                    var videoRequest = yt.Videos.List("statistics");
                    videoRequest.Id = playlistItem.Snippet.ResourceId.VideoId;
                    var videoResponse= videoRequest.Execute();
                    var toAdd = new VideoYoutube { VideoUrl = "https://www.youtube.com/embed/" + playlistItem.Snippet.ResourceId.VideoId, Title = playlistItem.Snippet.Title, likes = 0, dislikes = 0, viewCount = 0 };
                    if (videoResponse.Items.Count == 1)
                    {
                        toAdd.likes = videoResponse.Items[0].Statistics.LikeCount;
                        toAdd.dislikes = videoResponse.Items[0].Statistics.DislikeCount;
                        toAdd.viewCount = videoResponse.Items[0].Statistics.ViewCount;
                    }
                    toReturn.Add(toAdd);
                    Console.WriteLine(playlistItem.ToString());
                }
            }
            return toReturn;
        }
    }
}
