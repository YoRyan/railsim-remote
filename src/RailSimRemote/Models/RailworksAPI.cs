using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;

namespace RailSimRemote.Models
{
    public enum RailworksAPIGetType : int
    {
        Current = 0,
        Minimum = 1,
        Maximum = 2
    }
    public static class RailworksAPI
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetDllDirectory(string lpPathName);

        [DllImport("RailDriver64.dll")]
        public static extern void SetRailDriverConnected(bool connected);

        [DllImport("RailDriver64.dll")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler,
            MarshalType = "RailSimRemote.Models.StringReturnMarshaler")]
        public static extern string GetLocoName();

        [DllImport("RailDriver64.dll")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler,
            MarshalType="RailSimRemote.Models.StringReturnMarshaler")]
        public static extern string GetControllerList();

        [DllImport("RailDriver64.dll")]
        public static extern float GetControllerValue(int controllerId, int getType);

        [DllImport("RailDriver64.dll")]
        public static extern void SetControllerValue(int controllerId, float value);

        public static void Load()
        {
            // Try to read Railworks' location from the Registry.
            // In case of failure, use a sane default.
            const string fallbackPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\RailWorks";
            string railworksPath = (string)Registry.GetValue(
                "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 24010",
                "InstallLocation",
                fallbackPath);
            SetDllDirectory(Path.Combine(railworksPath, "plugins"));
            SetRailDriverConnected(true);
        }
    }
}