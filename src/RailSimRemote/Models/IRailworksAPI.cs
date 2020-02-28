namespace RailSimRemote.Models
{
    public interface IRailworksAPI
    {
        void SetRailDriverConnected(bool connected);
        string GetLocoName();
        string GetControllerList();
        float GetControllerValue(int controllerId, RailworksAPIGetType getType);
        void SetControllerValue(int controllerId, float value);
    }
}