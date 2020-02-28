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
            return RailworksStatics.GameData.GetLoco();
        }

        [ActionName("Controls")]
        public Dictionary<string, BriefControlDescription> GetControls()
        {
            var result = new Dictionary<string, BriefControlDescription>();
            foreach (KeyValuePair<string, ControlDescription> entry in RailworksStatics.GameData.GetControls())
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
            return RailworksStatics.GameData.GetControlValue(name);
        }

        [HttpPut]
        [ActionName("Control")]
        public void PutControl(string name, float value)
        {
            RailworksStatics.GameData.SetControlValue(name, value);
        }

        [ActionName("Virtual")]
        public float GetVirtualControl(string name)
        {
            const int getCurrent = (int)RailworksAPIGetType.Current;
            switch (name)
            {
                case "Latitude":
                    return RailworksStatics.Api.GetControllerValue(400, getCurrent);
                case "Longitude":
                    return RailworksStatics.Api.GetControllerValue(401, getCurrent);
                case "Fuel":
                    return RailworksStatics.Api.GetControllerValue(402, getCurrent);
                case "Tunnel":
                    return RailworksStatics.Api.GetControllerValue(403, getCurrent);
                case "Gradient":
                    return RailworksStatics.Api.GetControllerValue(404, getCurrent);
                case "Heading":
                    return RailworksStatics.Api.GetControllerValue(405, getCurrent);
                case "Hours":
                    return RailworksStatics.Api.GetControllerValue(406, getCurrent);
                case "Minutes":
                    return RailworksStatics.Api.GetControllerValue(407, getCurrent);
                case "Seconds":
                    return RailworksStatics.Api.GetControllerValue(408, getCurrent);
                default:
                    throw new ArgumentException("unknown virtual controller");
            }
        }
    }
}
