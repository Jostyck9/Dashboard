using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.youtube
{
    public class Video
    {
        public string VideoUrl { get; set; }
        public string Title { get; set; }
    }

    public class YoutubeModel
    {
        const string MYYOUTUBE_CHANNEL = "SampleChannel";
        const string MyYOUTUBE_DEVELOPER_KEY = "AIzaSyC3aU9QVuq8vtkYlj0cJzK0CyL8vZ5d_78";

        public static List<Video> getVideo()
        {
            List<Video> toReturn = new List<Video>();

            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = MyYOUTUBE_DEVELOPER_KEY });
            ChannelsResource.ListRequest channelsListRequest = yt.Channels.List("contentDetails");
            channelsListRequest.ForUsername = "aMOODIEsqueezie";
            var channelsListResponse = channelsListRequest.Execute();
            foreach (var channel in channelsListResponse.Items)
            {
                // of videos uploaded to the authenticated user's channel.
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                var nextPageToken = "";
                /*while (nextPageToken != null)
                {*/
                    var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                    playlistItemsListRequest.PlaylistId = uploadsListId;
                    playlistItemsListRequest.MaxResults = 50;
                    playlistItemsListRequest.PageToken = nextPageToken;
                    // Retrieve the list of videos uploaded to the authenticated user's channel.
                    var playlistItemsListResponse = playlistItemsListRequest.Execute();
                    foreach (var playlistItem in playlistItemsListResponse.Items)
                    {
                        toReturn.Add(new Video {VideoUrl = "https://www.youtube.com/embed/" + playlistItem.Snippet.ResourceId.VideoId, Title = playlistItem.Snippet.Title});
                        Console.WriteLine(playlistItem.ToString());
                        // Print information about each video.
                        //Console.WriteLine("Video Title= {0}, Video ID ={1}", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId);
                        /*var qry = (from s in ObjEdbContext.ObjTubeDatas where s.Title == playlistItem.Snippet.Title select s).FirstOrDefault();
                        if (qry == null)
                        {
                            objYouTubeData.VideoId = "https://www.youtube.com/embed/" + playlistItem.Snippet.ResourceId.VideoId;
                            objYouTubeData.Title = playlistItem.Snippet.Title;
                            objYouTubeData.Descriptions = playlistItem.Snippet.Description;
                            objYouTubeData.ImageUrl = playlistItem.Snippet.Thumbnails.High.Url;
                            objYouTubeData.IsValid = true;
                            ObjEdbContext.ObjTubeDatas.Add(objYouTubeData);
                            ObjEdbContext.SaveChanges();
                            ModelState.Clear();

                        }*/
                    /*}*/
                    nextPageToken = playlistItemsListResponse.NextPageToken;
                }
            }
            return toReturn;
        }
    }
}
