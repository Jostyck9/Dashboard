using Dashboard.Models.Steam;
using System;
using Xunit;

namespace XUnitTestProject
{
    public class TestSteamModel
    {
        private readonly SteamModel _sModel = new Dashboard.Models.Steam.SteamModel();

        [Fact]
        public async void TestGetGameList()
        {
            var res = await _sModel.GetGameList();

            Assert.NotNull(res);
            Assert.NotNull(res.Applist);
        }

        [Fact]
        public async void TestGetCurrentPlayersGameTrue()
        {
            var res = await _sModel.GetCurrentPlayersGame("440");
            ulong nbr = 0;

            Assert.NotNull(res);
            Assert.NotNull(res.Response);
            Assert.NotNull(res.Response.Player_count);
            Assert.NotEqual("", res.Response.Player_count);
            Assert.True(ulong.TryParse(res.Response.Player_count, out nbr));
        }

        [Fact]
        public async void TestGetCurrentPlayersGameFalse()
        {
            var res = await _sModel.GetCurrentPlayersGame("uyazgdedz");

            Assert.NotNull(res);
            Assert.NotNull(res.Response);
            Assert.Equal("0", res.Response.Player_count);
        }

        [Fact]
        public async void TestGetAchievementsGameTrue()
        {
            var res = await _sModel.GetAchievementGame("440");

            Assert.NotNull(res);
            Assert.NotNull(res.Achievements);
        }

        [Fact]
        public async void TestGetAchievementsGameFalse()
        {
            var res = await _sModel.GetAchievementGame("");

            Assert.NotNull(res);
            Assert.Null(res.Achievements);
        }

        [Fact]
        public async void TestGetLastNewsGameTrue()
        {
            var res = await _sModel.GetLastNews("440", 3);

            Assert.NotNull(res);
            Assert.NotNull(res.AppNews.NewsItems);
            Assert.Equal((ulong)440, res.AppNews.AppId);
            Assert.Equal(3, res.AppNews.NewsItems.Count);
        }

        [Fact]
        public async void TestGetLastNewsGameFalse()
        {
            var res = await _sModel.GetLastNews("", 3);

            Assert.NotNull(res);
            Assert.Null(res.AppNews);
        }

        [Fact]
        public async void TestSearchGame()
        {
            var res = await _sModel.SearchGame("Destiny");

            Assert.NotNull(res);
            Assert.NotEmpty(res);
        }
    }
}
