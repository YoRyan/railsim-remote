using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RailSimRemote.Models;

namespace RailSimRemote.Models.Tests
{
    [TestClass()]
    public class RailworksGameDataTests
    {
        [TestMethod()]
        public void Get_Invalid_Loco()
        {
            RailworksAPIMock mock = new RailworksAPIMock("Bad.:.LocoName");
            RailworksGameData gameData = new RailworksGameData(mock);
            LocoName loco = gameData.GetLoco();
            Assert.AreEqual(loco.Provider, "");
            Assert.AreEqual(loco.Product, "");
            Assert.AreEqual(loco.Engine, "");
        }

        [TestMethod()]
        public void Get_Valid_Loco()
        {
            RailworksAPIMock mock = new RailworksAPIMock("Provider..:.:Product..:.Engine:");
            RailworksGameData gameData = new RailworksGameData(mock);
            LocoName loco = gameData.GetLoco();
            Assert.AreEqual(loco.Provider, "Provider.");
            Assert.AreEqual(loco.Product, ":Product.");
            Assert.AreEqual(loco.Engine, "Engine:");
        }
    }
}