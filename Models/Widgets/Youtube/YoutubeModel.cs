﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Dashboard.Models.youtube
{
    public class YoutubeModel : IYoutubeModel
    {
        const string MyYOUTUBE_DEVELOPER_KEY = "AIzaSyC3aU9QVuq8vtkYlj0cJzK0CyL8vZ5d_78";
        const string BASE_URL = "https://www.youtube.com/embed/";
        private static readonly string[] validAuthorities = { "youtube.com", "www.youtube.com", "youtu.be", "www.youtu.be" };

        /**
        * @brief check if the string in paramater is a correct URL
        *
        * @return a bool
        */
        private bool ValidUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var query = HttpUtility.ParseQueryString(uri.Query);
                if (!validAuthorities.Contains(uri.Host))
                    return false;
            } catch (System.UriFormatException)
            {
                return false;
            }
            return true;
        }

        /**
        * @brief get the id of a video from an URL which is a string
        *
        * @return a string
        */
        public string GetIdVideo(string url)
        {
            if (url == null || !ValidUrl(url))
                return null;
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);
            if (query.AllKeys.Contains("v"))
            {
                return query["v"];
            }
            return null;
        }

        /**
        * @brief get all the informations of a video from an Id which is a string
        *
        * @return a structure of Video Youtube
        */
        public async Task<VideoYoutube> GetVideoById(string id)
        {
            VideoYoutube toReturn = new VideoYoutube { Dislikes = 0, Likes = 0, Title = "", VideoUrl = "", VideoId = "",  ViewCount = 0 };
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = MyYOUTUBE_DEVELOPER_KEY });
            var videoStats = yt.Videos.List("statistics");
            videoStats.Id = id;

            if (id.Length != 11)
                return toReturn;

            try
            {
                var videoStatsResponse = await videoStats.ExecuteAsync();
                if (videoStatsResponse.Items.Count == 1)
                {
                    toReturn.Title = "";
                    toReturn.VideoId = id;
                    toReturn.VideoUrl = BASE_URL + id.ToString();
                    toReturn.Likes = videoStatsResponse.Items[0].Statistics.LikeCount;
                    toReturn.Dislikes = videoStatsResponse.Items[0].Statistics.DislikeCount;
                    toReturn.ViewCount = videoStatsResponse.Items[0].Statistics.ViewCount;
                }
                else
                {
                    return toReturn;
                }

                var videoContent = yt.Videos.List("snippet");
                videoContent.Id = id;
                var videoContentResponse = await videoContent.ExecuteAsync();
                if (videoContentResponse.Items.Count == 1)
                {
                    toReturn.Title = videoContentResponse.Items[0].Snippet.Title;
                }

            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
            return toReturn;
        }

        /**
        * @brief get all the informations of a video from an URL which is a string
        *
        * @return a structure of Video Youtube
        */
        public async Task<VideoYoutube> GetVideoByUrl(string url)
        {
            VideoYoutube toReturn = new VideoYoutube { Dislikes = 0, Likes = 0, Title = "", VideoUrl = "", ViewCount = 0 };
            string id = GetIdVideo(url);

            if (id == null)
            {
                return toReturn;
            }
            return await GetVideoById(id);
        }

        /**
        * @brief get all videos from a research
        *
        * @return a list structure of Video Youtube
        */
        public async Task<List<VideoYoutube>> SearchVideos(string query, int maxRes = 50)
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
                var searchListResponse = await searchListRequest.ExecuteAsync();
                foreach (var searchResult in searchListResponse.Items)
                {
                    if (searchResult.Id.Kind == "youtube#video")
                    {
                        toReturn.Add(new VideoYoutube { Title = searchResult.Snippet.Title, VideoUrl = BASE_URL + searchResult.Id.VideoId, VideoId = searchResult.Id.VideoId, Dislikes = 0, Likes = 0, ViewCount = 0 });
                        /*toReturn.Add(await GetVideoById(searchResult.Id.VideoId)); //Too long to execute for each video*/
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return toReturn;
        }

        /**
        * @brief get all channels from a research
        *
        * @return a list structure of Video Youtube
        */
        public async Task<List<ChannelYoutube>> SearchChannels(string query, int maxRes = 20)
        {
            var toReturn = new List<ChannelYoutube>();
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
                var searchListResponse = await searchListRequest.ExecuteAsync();
                foreach (var searchResultChannel in searchListResponse.Items)
                {
                    if (searchResultChannel.Id.Kind == "youtube#channel")
                    {
                        toReturn.Add(await GetChannelById(searchResultChannel.Id.ChannelId));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return toReturn;
        }

        /**
        * @brief get all the informations of a channel from an Id which is a string
        *
        * @return a structure of Video Youtube
        */
        public async Task<ChannelYoutube> GetChannelById(string id)
        {
            ChannelYoutube toReturn = new ChannelYoutube {Id = "", SubCount = 0, Title = ""};
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = MyYOUTUBE_DEVELOPER_KEY });
            var channelStats = yt.Channels.List("statistics");
            channelStats.Id = id;

            if (id.Length != 24)
                return toReturn;

            try
            {
                var channelStatsResponse = await channelStats.ExecuteAsync();
                if (channelStatsResponse.Items.Count == 1)
                {
                    toReturn.Title = "";
                    toReturn.Banner = "";
                    toReturn.SubCount = channelStatsResponse.Items[0].Statistics.SubscriberCount;
                    toReturn.Id = id;
                }
                else
                {
                    return toReturn;
                }

                var channelContent = yt.Channels.List("snippet");
                channelContent.Id = id;
                var channelContentResponse = await channelContent.ExecuteAsync();
                if (channelContentResponse.Items.Count == 1)
                {
                    toReturn.Title = channelContentResponse.Items[0].Snippet.Title;
                }

                var channelImage = yt.Channels.List("brandingSettings");
                channelImage.Id = id;
                var channelImageResponse = await channelImage.ExecuteAsync();
                if (channelImageResponse.Items.Count == 1)
                {
                    toReturn.Banner = channelImageResponse.Items[0].BrandingSettings.Image.BannerImageUrl;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return toReturn;
        }
    }
}
