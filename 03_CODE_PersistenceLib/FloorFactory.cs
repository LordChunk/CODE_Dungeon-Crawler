using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.FloorTiles;
using CODE_GameLib.Interfaces;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class FloorFactory
    {
        /// <summary>
        /// A list of all floor tiles types with their corresponding parser methods. Each method takes a JToken as it's parameter and outputs an IFloorTile.
        /// </summary>
        private static readonly Dictionary<string, Func<JToken, IFloorTile>> _itemTypes = new Dictionary<string, Func<JToken, IFloorTile>>
        {
            { "ice", CreateIceFloorTile },
        };

        /// <summary>
        /// Iterates through a set of JSON strings describing floor tiles and parses them.
        /// </summary>
        /// <param name="jsonFloorTiles">A set of JSON strings describing floor tiles</param>
        /// <returns>List of floor tiles parsed from the jsonItems parameter</returns>
        public static List<IFloorTile> CreateFloorTiles(IEnumerable<JToken> jsonFloorTiles)
        {
            return jsonFloorTiles.Select(CreateFloorTile).ToList();
        }

        public static IFloorTile CreateFloorTile(JToken jsonFloorTile)
        {
            var type = jsonFloorTile["type"].Value<string>();
            // Check if item type is valid
            var typeKvp = _itemTypes.FirstOrDefault(it => it.Key == type);
            if (typeKvp.Value == null) throw new NoNullAllowedException("Item type " + type + " is not a valid item type.");
            // Parse item and return
            return typeKvp.Value(jsonFloorTile);
        }

        private static IFloorTile CreateIceFloorTile(JToken jsonFloorTile)
        {
            return new IceFloorTile(GetFloorTIleCoordinates(jsonFloorTile));
        }

        private static Coordinate GetFloorTIleCoordinates(JToken jsonFloorTile)
        {
            return new Coordinate(
                jsonFloorTile["x"].Value<int>(),
                jsonFloorTile["y"].Value<int>()
            );
        }
    }
}
