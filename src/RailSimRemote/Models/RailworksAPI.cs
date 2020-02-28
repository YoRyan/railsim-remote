namespace RailSimRemote.Models
{
    public class RailworksAPI : IRailworksAPI
    {
        public RailworksAPI()
        {
            RailDriverDLL.Load();
        }

        public string GetControllerList()
        {
            return RailDriverDLL.GetControllerList();
        }

        public float GetControllerValue(int controllerId, RailworksAPIGetType getType)
        {
            switch (getType)
            {
                case RailworksAPIGetType.Current:
                    return RailDriverDLL.GetControllerValue(controllerId, 0);
                case RailworksAPIGetType.Minimum:
                    return RailDriverDLL.GetControllerValue(controllerId, 1);
                case RailworksAPIGetType.Maximum:
                    return RailDriverDLL.GetControllerValue(controllerId, 2);
                default:
                    return 0;
            }
        }

        public string GetLocoName()
        {
            return RailDriverDLL.GetLocoName();
        }

        public void SetControllerValue(int controllerId, float value)
        {
            RailDriverDLL.SetControllerValue(controllerId, value);
        }

        public void SetRailDriverConnected(bool connected)
        {
            RailDriverDLL.SetRailDriverConnected(connected);
        }
    }
}