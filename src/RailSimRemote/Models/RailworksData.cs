using System;
using System.Collections.Generic;

namespace RailSimRemote.Models
{
    public struct ControlDescription
    {
        public int Id;
        public float Minimum;
        public float Maximum;
    }
    public struct LocoName
    {
        public string Provider;
        public string Product;
        public string Engine;
        public LocoName(string str)
        {
            string[] split = str.Split(new string[] { ".:." }, 3, StringSplitOptions.None);
            if (split.Length < 3)
            {
                Provider = Product = Engine = "";
            }
            else
            {
                Provider = split[0];
                Product = split[1];
                Engine = split[2];
            }
        }
    }
    public static class RailworksData
    {
        private static LocoName loco;
        private static Dictionary<string, ControlDescription> controls = new Dictionary<string, ControlDescription>();
        private static readonly object mutex = new object();
        public static void Load()
        {
            RailworksAPI.Load();
        }
        public static LocoName GetLoco()
        {
            lock (mutex)
            {
                CheckLoco();
                return loco;
            }
        }
        public static Dictionary<string, ControlDescription> GetControls()
        {
            lock (mutex)
            {
                CheckLoco();
                return new Dictionary<string, ControlDescription>(controls);
            }
        }
        public static float GetControlValue(string name)
        {
            ControlDescription control;
            lock (mutex)
            {
                CheckLoco();
                control = controls[name];
            }
            return RailworksAPI.GetControllerValue(control.Id, (int)RailworksAPIGetType.Current);
        }
        public static void SetControlValue(string name, float value)
        {
            ControlDescription control;
            lock (mutex)
            {
                CheckLoco();
                control = controls[name];
            }
            if (value < control.Minimum || value > control.Maximum)
            {
                throw new ArgumentException("value out of bounds");
            }
            RailworksAPI.SetControllerValue(control.Id, value);
        }
        private static void CheckLoco()
        {
            lock (mutex)
            {
                string currentLoco = RailworksAPI.GetLocoName();
                if (loco.Equals(currentLoco))
                {
                    return;
                }

                controls.Clear();
                string controlList = RailworksAPI.GetControllerList();
                string[] controlNames = controlList.Split(new string[] { "::" }, StringSplitOptions.None);
                for (int i = 0; i < controlNames.Length; i++)
                {
                    string name = controlNames[i];
                    float min = RailworksAPI.GetControllerValue(i, (int)RailworksAPIGetType.Minimum);
                    float max = RailworksAPI.GetControllerValue(i, (int)RailworksAPIGetType.Maximum);
                    controls[name] = new ControlDescription { Id = i, Minimum = min, Maximum = max };
                }

                loco = new LocoName(currentLoco);
            }
        }
    }
}