using System;
using System.Collections.Generic;
using System.Web.Http;

using RailSimRemote.Models;

namespace RailSimRemote.Controllers
{
    public struct BriefControlDescription
    {
        public float Minimum;
        public float Maximum;
    }
    public class RailworksController : ApiController
    {
        [ActionName("Loco")]
        public LocoName GetLoco()
        {
            return RailworksData.GetLoco();
        }

        [ActionName("Controls")]
        public Dictionary<string, BriefControlDescription> GetControls()
        {
            var result = new Dictionary<string, BriefControlDescription>();
            foreach (KeyValuePair<string, ControlDescription> entry in RailworksData.GetControls())
            {
                result[entry.Key] = new BriefControlDescription
                {
                    Minimum = entry.Value.Minimum,
                    Maximum = entry.Value.Maximum
                };
            }
            return result;
        }

        [HttpGet]
        [ActionName("Control")]
        public float GetControl(string name)
        {
            return RailworksData.GetControlValue(name);
        }

        [HttpPut]
        [ActionName("Control")]
        public void PutControl(string name, float value)
        {
            RailworksData.SetControlValue(name, value);
        }

        [ActionName("Virtual")]
        public float GetVirtualControl(string name)
        {
            const int getCurrent = (int)RailworksAPIGetType.Current;
            switch (name)
            {
                case "Latitude":
                    return RailworksAPI.GetControllerValue(400, getCurrent);
                case "Longitude":
                    return RailworksAPI.GetControllerValue(401, getCurrent);
                case "Fuel":
                    return RailworksAPI.GetControllerValue(402, getCurrent);
                case "Tunnel":
                    return RailworksAPI.GetControllerValue(403, getCurrent);
                case "Gradient":
                    return RailworksAPI.GetControllerValue(404, getCurrent);
                case "Heading":
                    return RailworksAPI.GetControllerValue(405, getCurrent);
                case "Hours":
                    return RailworksAPI.GetControllerValue(406, getCurrent);
                case "Minutes":
                    return RailworksAPI.GetControllerValue(407, getCurrent);
                case "Seconds":
                    return RailworksAPI.GetControllerValue(408, getCurrent);
                default:
                    throw new ArgumentException("unknown virtual controller");
            }
        }
    }
}
