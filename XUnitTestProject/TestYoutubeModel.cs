using Dashboard.Models.youtube;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class TestYoutubeModel
    {
        private readonly YoutubeModel _ytModel = new YoutubeModel();

        [Fact]
        public void TestGetIdVideo()
        {
            var res = _ytModel.GetIdVideo("https://www.youtube.com/");
            Assert.Null(res);
            _ytModel.GetIdVideo("youtube.com/");
            Assert.Null(res);
            _ytModel.GetIdVideo("hfhvdhnjzdml;:azd!");
            Assert.Null(res);
            _ytModel.GetIdVideo("");
            Assert.Null(res);
            res = _ytModel.GetIdVideo("https://www.youtube.com/watch?v=ZyWt3kANA3Q");
            Assert.Equal("ZyWt3kANA3Q", res);
            res = _ytModel.GetIdVideo("https://www.youtu.be/watch?v=KaqC5FnvAEc");
            Assert.Equal("KaqC5FnvAEc", res);
        }

        [Fact]
        public async void TestGetVideoById()
        {
            var res = await _ytModel.GetVideoById("xZuFsP3LezE");

            Assert.NotNull(res);
            Assert.NotEqual("", res.Title);
            Assert.NotEqual("", res.VideoUrl);
            Assert.Contains("xZuFsP3LezE", res.VideoUrl);
            Assert.Equal("xZuFsP3LezE", res.VideoId);

            res = await _ytModel.GetVideoById("ezdiojdoijfexZuFsP3LezE");

            Assert.NotNull(res);
            Assert.Equal("", res.VideoId);
        }

        [Fact]
        public async void TestGetVideoByUrl()
        {
            var res = await _ytModel.GetVideoByUrl("https://www.youtube.com/watch?v=Lme7Qt7SdCw");

            Assert.NotNull(res);
            Assert.NotEqual("", res.Title);
            Assert.NotEqual("", res.VideoUrl);
            Assert.Contains("Lme7Qt7SdCw", res.VideoUrl);
            Assert.Equal("Lme7Qt7SdCw", res.VideoId);

            res = await _ytModel.GetVideoById("https://www.youtube.com/watch?v=Lme7Qt7SdCwedzzdzefefef");

            Assert.NotNull(res);
            Assert.Equal("", res.VideoId);
        }

        [Fact]
        public async void TestGetChannelById()
        {
            var res = await _ytModel.GetChannelById("UCpnkp_D4FLPCiXOmDhoAeYA");

            Assert.NotNull(res);
            Assert.NotEqual("", res.Title);
            Assert.Equal("UCpnkp_D4FLPCiXOmDhoAeYA", res.Id);
            Assert.NotEqual((ulong)0, res.SubCount);

            res = await _ytModel.GetChannelById("UCpnkp_D4FLPCiXOmDhoAeYAzejojdziuefoizjefoijzeoifjoizejf,");

            Assert.NotNull(res);
            Assert.Equal("", res.Title);
        }

        [Fact]
        public async void TestSearchVideos()
        {
            var res = await _ytModel.SearchVideos("Pokemon");

            Assert.NotNull(res);
            Assert.NotEmpty(res);

            Assert.NotEqual("", res[0].Title);

            
            res = await _ytModel.SearchVideos("");
            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public async void TestSearchChannels()
        {
            var res = await _ytModel.SearchChannels("jdg");

            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.NotEqual("", res[0].Title);

            res = await _ytModel.SearchChannels("");
            Assert.NotNull(res);
            Assert.Empty(res);
        }
    }
}
