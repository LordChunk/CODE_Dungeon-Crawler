using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Enums;
using CODE_GameLib.Tiles;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class ConveyorBeltFactory
    {
        public static List<ConveyorBelt> CreateConveyorBelts(IEnumerable<JToken> jsonItems)
        {
            return jsonItems.Select(CreateConveyorBelt).ToList();
        }

        public static ConveyorBelt CreateConveyorBelt(JToken jsonBelt)
        {
            var direction = (Direction) Enum.Parse(typeof(Direction), jsonBelt.Value<string>("direction"), true);
            return new ConveyorBelt(direction);
        }
    }
}