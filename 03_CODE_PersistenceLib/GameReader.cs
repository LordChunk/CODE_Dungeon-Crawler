using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using CODE_GameLib;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using CODE_GameLib.Items.Common;
using Newtonsoft.Json.Linq;
// ReSharper disable AssignNullToNotNullAttribute

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        private static Dictionary<string, Func<JObject, IItem>> ItemTypes = new Dictionary<string, Func<JObject, IItem>>
        {
            { "boobietrap", CreateTrapFromJson }
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

                foreach (var jsonItem in jsonRoom["items"].Values())
                {
                    var type = jsonItem["type"].Value<string>();

                }

                rooms.Add(room);
            }


            return new Game();
        }


        private static Room CreateRoomFromJson(JToken jsonRoom)
        {
            return new Room
            {
                Id = jsonRoom["id"].Value<int>(),
                Height = jsonRoom["height"].Value<int>(),
                Width = jsonRoom["width"].Value<int>(),
                Doors = new Dictionary<Direction, IDoor>(),
                Items = new List<Item>()
            };
        }

        private static Item CreateItemFromJson(JToken jsonItem)
        {
            var type = jsonItem["type"].Value<string>();
            

            return null;
        }

        private static Trap CreateTrapFromJson(JToken jsonTrap)
        {
            return null;
        }
    }
}
