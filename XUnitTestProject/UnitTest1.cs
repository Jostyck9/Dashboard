using System;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var fact = new Dashboard.Models.Widgets.WidgetFactory();
            var modelTest = new Dashboard.Models.youtube.YoutubeModel();

            var res = await modelTest.GetVideoById("wZZ7oFKsKzY");
        }
    }
}
