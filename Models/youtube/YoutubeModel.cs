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
        const string MyYOUTUBE_DEVELOPER_KEY = "AIzaSyC3aU9QVuq8vtkYlj0cJzK0CyL8vZ5d_78";
        const string BASE_URL = "https://www.youtube.com/embed/";
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

        public VideoYoutube GetVideoById(string id)
        {
            VideoYoutube toReturn = new VideoYoutube { dislikes = 0, likes = 0, Title = "", VideoUrl = "", viewCount = 0 };
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = MyYOUTUBE_DEVELOPER_KEY });
            var videoStats = yt.Videos.List("statistics");
            videoStats.Id = id;

            if (id.Length != 11)
                return toReturn;

            try
            {
                var videoStatsResponse = videoStats.Execute();
                if (videoStatsResponse.Items.Count == 1)
                {
                    toReturn.Title = "";
                    toReturn.VideoUrl = BASE_URL + id.ToString();
                    toReturn.likes = videoStatsResponse.Items[0].Statistics.LikeCount;
                    toReturn.dislikes = videoStatsResponse.Items[0].Statistics.DislikeCount;
                    toReturn.viewCount = videoStatsResponse.Items[0].Statistics.ViewCount;
                }
                else
                {
                    return toReturn;
                }

                var videoContent = yt.Videos.List("snippet");
                videoContent.Id = id;
                var videoContentResponse = videoContent.Execute();
                if (videoContentResponse.Items.Count == 1)
                {
                    toReturn.Title = videoContentResponse.Items[0].Snippet.Title;
                }

            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return toReturn;
        }

        public VideoYoutube GetVideoByUrl(string url)
        {
            VideoYoutube toReturn = new VideoYoutube { dislikes = 0, likes = 0, Title = "", VideoUrl = "", viewCount = 0 };
            string id = GetIdVideo(url);

            if (id == null)
            {
                return toReturn;
            }
            return GetVideoById(id);
        }

        public List<VideoYoutube> SearchVideos(string query, int maxRes = 50)
        {
            var toReturn = new List<VideoYoutube>();
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = MyYOUTUBE_DEVELOPER_KEY });
            var searchListRequest = yt.Search.List("snippet");

            if (query == string.Empty)
            {
                return toReturn;
            }
            searchListRequest.Q = query; // Replace with your search term.
            searchListRequest.MaxResults = maxRes;
            try
            {
                var searchListResponse = searchListRequest.Execute();
                foreach (var searchResult in searchListResponse.Items)
                {
                    if (searchResult.Id.Kind == "youtube#video")
                    {
                        toReturn.Add(new VideoYoutube { Title = searchResult.Snippet.Title, VideoUrl = BASE_URL + searchResult.Id.VideoId, dislikes = 0, likes = 0, viewCount = 0 });
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return toReturn;
        }
    }
}
