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
        [Route("api/railworks/control/{name}")]
        public float GetControl(string name)
        {
            return RailworksStatics.GameData.GetControlValue(name);
        }

        [HttpPut]
        [Route("api/railworks/control/{name}")]
        public void PutControl(string name, float value)
        {
            RailworksStatics.GameData.SetControlValue(name, value);
        }

        [Route("api/railworks/virtual/{name}")]
        public float GetVirtualControl(string name)
        {
            var controls = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                { "Latitude", 400 },
                { "Longitude", 401 },
                { "Fuel", 402 },
                { "Tunnel", 403 },
                { "Gradient", 404 },
                { "Heading", 405 },
                { "Hours", 406 },
                { "Minutes", 407 },
                { "Seconds", 408 }
            };
            return RailworksStatics.Api.GetControllerValue(
                controls[name], (int)RailworksAPIGetType.Current);
        }
    }
}
