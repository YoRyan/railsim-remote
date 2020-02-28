using RailSimRemote.Models;

namespace RailSimRemote.Models.Tests
{
    class RailworksAPIMock : IRailworksAPI
    {
        public string LocoName;
        public string ControllerList;
        public float[] ControlValues;

        public RailworksAPIMock(string locoName)
        {
            LocoName = locoName;
            ControllerList = "";
        }

        public RailworksAPIMock(string locoName, string[] controls)
        {
            LocoName = locoName;
            ControllerList = string.Join("::", controls);
            ControlValues = new float[controls.Length];
        }

        public string GetControllerList()
        {
            return ControllerList;
        }

        public float GetControllerValue(int controllerId, RailworksAPIGetType getType)
        {
            // Don't bother with minimum/maximum values.
            return ControlValues[controllerId];
        }

        public string GetLocoName()
        {
            return LocoName;
        }

        public void SetControllerValue(int controllerId, float value)
        {
            ControlValues[controllerId] = value;
        }

        public void SetRailDriverConnected(bool connected) {}
    }
}
