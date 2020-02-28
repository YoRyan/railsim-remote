using RailSimRemote.Models;

namespace RailSimRemote.Controllers
{
    public static class RailworksStatics
    {
        public static RailworksAPI Api = new RailworksAPI();
        public static RailworksGameData GameData = new RailworksGameData(Api);
    }
}