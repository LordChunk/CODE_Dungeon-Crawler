﻿using CODE_GameLib;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using CODE_GameLib.Items.Adapter;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_PersistenceLib
{
    public class ItemFactory
    {
        private const int EnemyLives = 3;

        /// <summary>
        /// A list of all item types with their corresponding parser methods. Each method takes a JToken as it's parameter and outputs an IItem.
        /// </summary>
        private static readonly Dictionary<string, Func<JToken, IItem>> _itemTypes = new Dictionary<string, Func<JToken, IItem>>
        {
            { "boobietrap", CreateTrap },
            { "disappearing boobietrap", CreateSingleUseTrap },
            { "sankara stone", CreateSankaraStone },
            { "key", CreateKey },
            { "pressure plate", CreatePressurePlate },
            { "horizontal", CreateHorizontalEnemy },
            { "vertical", CreateVerticalEnemy },
        };


        /// <summary>
        /// Iterates through a set of JSON strings describing items and parses them.
        /// </summary>
        /// <param name="jsonItems">A set of JSON strings describing items</param>
        /// <returns>List of items parsed from the jsonItems parameter</returns>
        public static List<IItem> CreateItems(IEnumerable<JToken> jsonItems)
        {
            return jsonItems.Select(CreateItem).ToList();
        }

        /// <summary>
        /// Parses a JSON string containing an item. This method will check the item against all item types stored in the ItemTypes dictionary
        /// </summary>
        /// <param name="jsonItem">JSON string containing the item</param>
        /// <returns>Parsed item</returns>
        private static IItem CreateItem(JToken jsonItem)
        {
            var type = jsonItem["type"].Value<string>();
            // Check if item type is valid
            var typeKvp = _itemTypes.FirstOrDefault(it => it.Key == type);
            if (typeKvp.Value == null) throw new NoNullAllowedException("Item type " + type + " is not a valid item type.");
            // Parse item and return
            var item = typeKvp.Value(jsonItem);
            return item;
        }

        private static Coordinate GetItemCoordinates(JToken jsonItem)
        {
            return new Coordinate(
                jsonItem["x"].Value<int>(),
                jsonItem["y"].Value<int>()
            );
        }

        #region Item create methods
        private static Trap CreateTrap(JToken jsonTrap)
        {
            return new Trap(GetItemCoordinates(jsonTrap), jsonTrap["damage"].Value<int>());
        }

        private static SingleUseTrap CreateSingleUseTrap(JToken jsonTrap)
        {
            return new SingleUseTrap(GetItemCoordinates(jsonTrap), jsonTrap["damage"].Value<int>());
        }

        private static SankaraStone CreateSankaraStone(JToken jsonStone)
        {
            return new SankaraStone(GetItemCoordinates(jsonStone));
        }

        private static Key CreateKey(JToken jsonKey)
        {
            return new Key(GetItemCoordinates(jsonKey), ParseColorString(jsonKey["color"].Value<string>()));
        }

        private static PressurePlate CreatePressurePlate(JToken jsonPlate)
        {
            return new PressurePlate(GetItemCoordinates(jsonPlate));
        }

        private static Color ParseColorString(string color)
        {
            return Color.FromName(color);
        }

        private static IItem CreateHorizontalEnemy(JToken jsonEnemy)
        {
            return new EnemyAdapter(new HorizontallyMovingEnemy(EnemyLives, (int)jsonEnemy["x"], (int)jsonEnemy["y"], (int)jsonEnemy["minX"], (int)jsonEnemy["maxX"]));
        }

        private static IItem CreateVerticalEnemy(JToken jsonEnemy)
        {
            return new EnemyAdapter(new VerticallyMovingEnemy(EnemyLives, (int)jsonEnemy["x"], (int)jsonEnemy["y"], (int)jsonEnemy["minY"], (int)jsonEnemy["maxY"]));
        }
        #endregion
    }
}