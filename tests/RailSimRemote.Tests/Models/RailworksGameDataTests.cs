using Xunit;

using RailSimRemote.Models;

namespace RailSimRemote.Tests.Models
{
    public class RailworksGameDataTests
    {
        [Fact]
        public void Get_Invalid_Loco()
        {
            RailworksAPIMock mock = new RailworksAPIMock("Bad.:.LocoName");
            RailworksGameData gameData = new RailworksGameData(mock);
            LocoName loco = gameData.GetLoco();
            Assert.Equal("", loco.Provider);
            Assert.Equal("", loco.Product);
            Assert.Equal("", loco.Engine);
        }

        [Fact]
        public void Get_Valid_Loco()
        {
            RailworksAPIMock mock = new RailworksAPIMock("Provider..:.:Product..:.Engine:");
            RailworksGameData gameData = new RailworksGameData(mock);
            LocoName loco = gameData.GetLoco();
            Assert.Equal("Provider.", loco.Provider);
            Assert.Equal(":Product.", loco.Product);
            Assert.Equal("Engine:", loco.Engine);
        }
    }
}
