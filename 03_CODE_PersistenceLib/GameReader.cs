using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using CODE_GameLib.Items.Common;
using Newtonsoft.Json.Linq;
// ddReSharper disable AssignNullToNotNullAttribute

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        /// <summary>
        /// A list of all item types with their corresponding parser methods. Each method takes a JToken as it's parameter and outputs an IItem.
        /// </summary>
        private static Dictionary<string, Func<JToken, IItem>> ItemTypes = new Dictionary<string, Func<JToken, IItem>>
        {
            { "boobietrap", CreateTrap },
            { "disappearing boobietrap", CreateSingleUseTrap },
            { "sankara stone", null },
            { "key", null },
            { "pressure plate", null },
        }; 

        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));
            var jsonRooms = json["rooms"];
            var rooms = new List<Room>();

            if (jsonRooms == null) throw new NoNullAllowedException("This level contains no rooms.");
            foreach (var jsonRoom in jsonRooms)
            {
                if(jsonRoom["type"]?.ToString() != "room") continue;
                var room = CreateRoomFromJson(jsonRoom);
                room.Items = ParseItems(jsonRoom["items"].Values());
                // TODO: Parse doors

                rooms.Add(room);
            }


            return new Game();
        }

        private List<IItem> ParseItems(IEnumerable<JToken> jsonItems)
        {
            var items = new List<IItem>();
            foreach (var jsonItem in jsonItems)
            {
                var item = ItemTypes.FirstOrDefault(
                        i => i.Key == jsonItem["type"].Value<string>())
                    .Value(jsonItem);
                items.Add(item);
            }

            return items;
        }


        private static Room CreateRoomFromJson(JToken jsonRoom)
        {
            return new Room
            {
                Id = jsonRoom["id"].Value<int>(),
                Height = jsonRoom["height"].Value<int>(),
                Width = jsonRoom["width"].Value<int>(),
                Doors = new Dictionary<Direction, IDoor>(),
                Items = new List<IItem>()
            };
        }

        private static Item CreateItem(JToken jsonItem)
        {
            var type = jsonItem["type"].Value<string>();
            

            return null;
        }

        //private static IItem SetItemCoordinates(IItem item, JToken jsonItem)
        //{
        //    //return new 
        //}

        private static Trap CreateTrap(JToken jsonTrap)
        {
            return new Trap
            {
                X = jsonTrap["x"].Value<int>(),
                Y = jsonTrap["y"].Value<int>(),
                Damage = jsonTrap["damage"].Value<int>(),
            };
        }

        private static SingleUseTrap CreateSingleUseTrap(JToken jsonTrap)
        {
            return new SingleUseTrap
            {
                X = jsonTrap["x"].Value<int>(),
                Y = jsonTrap["y"].Value<int>(),
                Damage = jsonTrap["damage"].Value<int>(),
            };
        }

        private static SankaraStone createSankaraStone(JToken jsonStone)
        {
            //return new SankaraStone
            //{
                
            //}

            return null;
        }
    }
}
