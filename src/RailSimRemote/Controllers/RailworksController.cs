using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using RailSimRemote.Models;

namespace RailSimRemote.Controllers
{
    public class RailworksController : Controller
    {
        [Route("api/railworks/loco")]
        public LocoName GetLoco()
        {
            return RailworksStatics.GameData.GetLoco();
        }

        [Route("api/railworks/controls")]
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
        [Route("api/railworks/control")]
        public float GetControl(string name)
        {
            return RailworksStatics.GameData.GetControlValue(name);
        }

        [HttpPut]
        [Route("api/railworks/control")]
        public void PutControl(string name, float value)
        {
            RailworksStatics.GameData.SetControlValue(name, value);
        }

        [Route("api/railworks/virtual")]
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
