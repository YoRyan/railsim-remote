using System;
using System.Collections.Generic;

namespace RailSimRemote.Models
{
    public class RailworksGameData
    {
        private IRailworksAPI api;
        private string locoString = "";
        private LocoName loco = new LocoName("");
        private Dictionary<string, ControlDescription> controls = new Dictionary<string, ControlDescription>(
            StringComparer.OrdinalIgnoreCase);
        private readonly object mutex = new object();

        public RailworksGameData(IRailworksAPI api)
        {
            this.api = api;
        }

        public LocoName GetLoco()
        {
            lock (mutex)
            {
                CheckLoco();
                return loco;
            }
        }

        public Dictionary<string, ControlDescription> GetControls()
        {
            lock (mutex)
            {
                CheckLoco();
                return new Dictionary<string, ControlDescription>(controls);
            }
        }

        public float GetControlValue(string name)
        {
            ControlDescription control;
            lock (mutex)
            {
                CheckLoco();
                control = controls[name];
            }
            return api.GetControllerValue(control.Id, RailworksAPIGetType.Current);
        }

        public void SetControlValue(string name, float value)
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
            api.SetControllerValue(control.Id, value);
        }

        private void CheckLoco()
        {
            lock (mutex)
            {
                string currentLoco = api.GetLocoName();
                if (locoString.Equals(currentLoco))
                {
                    return;
                }
                locoString = currentLoco;
                loco = new LocoName(currentLoco);

                controls.Clear();
                string controlList = api.GetControllerList();
                string[] controlNames = controlList.Split(new string[] { "::" }, StringSplitOptions.None);
                for (int i = 0; i < controlNames.Length; i++)
                {
                    string name = controlNames[i];
                    if (name.Length == 0)
                    {
                        continue;
                    }

                    float min = api.GetControllerValue(i, RailworksAPIGetType.Minimum);
                    float max = api.GetControllerValue(i, RailworksAPIGetType.Maximum);
                    controls[name] = new ControlDescription { Id = i, Minimum = min, Maximum = max };
                }
            }
        }
    }
}