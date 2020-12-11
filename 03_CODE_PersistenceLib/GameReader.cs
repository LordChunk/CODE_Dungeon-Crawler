using CODE_GameLib;
using CODE_GameLib.Doors.Common;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using CODE_GameLib.Doors;

// ddReSharper disable AssignNullToNotNullAttribute

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        /// <summary>
        /// A list of all item types with their corresponding parser methods. Each method takes a JToken as it's parameter and outputs an IItem.
        /// </summary>
        private Dictionary<string, Func<JToken, IItem>> ItemTypes = new Dictionary<string, Func<JToken, IItem>>
        {
            { "boobietrap", CreateTrap },
            { "disappearing boobietrap", CreateSingleUseTrap },
            { "sankara stone", CreateSankaraStone },
            { "key", CreateKey },
            { "pressure plate", CreatePressurePlate },
        };

        private Dictionary<string, Func<IDoor>> DoorTypes = new Dictionary<string, Func<IDoor>>
        {
            { "colored", () => new ColorCodedDoor() },
            { "toggle", () => new ToggleDoor() },
            { "closing gate", () => new SingleUseDoor() }
        };

        private Dictionary<int, Room> rooms;

        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));
            var jsonRooms = json["rooms"];
            Room startRoom;

            // Parse rooms
            rooms = new Dictionary<int, Room>();
            if (jsonRooms == null) throw new NoNullAllowedException("This level contains no rooms.");
            foreach (var jsonRoom in jsonRooms)
            {
                if (jsonRoom["type"]?.ToString() != "room") continue;
                var room = CreateRoom(jsonRoom);
                var jsonItems = jsonRoom["items"];
                if (jsonItems != null)
                    room.Items = CreateItems(jsonItems);
                rooms.Add(room.Id, room);
            }

            // Parse doors/connections
            var jsonConnections = json["connections"].ToList();

            // Create doors and add them to rooms
            jsonConnections.ForEach(CreateDoorSet);

            // TODO: Add player to and set starter room

            return new Game();
        }

        /// <summary>
        /// Creates Door instances and links them to each other and to their respective room
        /// </summary>
        /// <param name="jsonConnection">JSON string containing all connections</param>
        private void CreateDoorSet(JToken jsonConnection)
        {
            IDoor door1;
            IDoor door2;
            var jsonDoor = jsonConnection["door"];
            if (jsonDoor != null)
            {
                // Get door type
                var type = jsonDoor["type"].Value<string>();
                door1 = DoorTypes.FirstOrDefault(kvp => kvp.Key == type).Value();
                door2 = DoorTypes.FirstOrDefault(kvp => kvp.Key == type).Value();
            }
            else
            {
                door1 = new Door();
                door2 = new Door();
            }
            // Connect doors to each other
            door1.ConnectsToDoor = door2;
            door2.ConnectsToDoor = door1;

            var connection1 = jsonConnection.First;
            var connection2 = jsonConnection.First.Next;

            // Parse definitions for first JSON line
            var locationStringDoor2 = connection1.ToObject<JProperty>()?.Name;
            var room1 = GetRoomFromId((int) connection1.First);
            var location2 = (Direction)Enum.Parse(typeof(Direction), locationStringDoor2, true);

            // Parse definitions for 2nd JSON line
            var locationStringDoor1 = connection2.ToObject<JProperty>()?.Name;
            var room2 = GetRoomFromId((int) connection2.First);
            var location1 = (Direction)Enum.Parse(typeof(Direction), locationStringDoor1, true);

            door1.IsInRoom = room1;
            door1.Location = location1;

            door2.IsInRoom = room2;
            door2.Location = location2;

            // TODO: Remove debug code
            //Console.WriteLine("Door 1");
            //Console.WriteLine("type: "+ door1.GetType());
            //Console.WriteLine("room id: " + door1.IsInRoom.Id);
            //Console.WriteLine("location in room: " + door1.Location);

            //Console.WriteLine("Door 2");
            //Console.WriteLine("type: " + door2.GetType());
            //Console.WriteLine("room id:" + door2.IsInRoom.Id);
            //Console.WriteLine("location in room:" + door2.Location);

            room1.Connections.Add(location1, door1);
            room2.Connections.Add(location2, door2);
        }

        private Room GetRoomFromId(int id)
        {
            return rooms.FirstOrDefault(kvp => kvp.Key == id).Value;
        }


        //private IConnection CreateConnection(JToken jsonConnection)
        //{
        //    IConnection connection;
        //    // Check if connection between room is "special connection"
        //    var jsonDoor = jsonConnection["door"];
        //    if (jsonDoor != null)
        //    {
        //        // Get door type
        //        var type = jsonDoor["type"].Value<string>();

        //        var (_, roomConnection) = ConnectionTypes.FirstOrDefault(kvp => kvp.Key == type);
        //        connection = roomConnection ?? throw new NoNullAllowedException("Connection type " + type + " is not a valid connection type.");
        //    }
        //    else
        //    {
        //        connection = new Connection();
        //    }

        //    // Add two connections to the rooms
        //    connection.Rooms = new List<Connection.RoomDirectionPair>
        //    {
        //        CreateConnectionPair(jsonConnection.First), 
        //        CreateConnectionPair(jsonConnection.First.Next)
        //    };

        //    return connection;
        //}

        //private Connection.RoomDirectionPair CreateConnectionPair(JToken jsonConnection)
        //{
        //    var directionString = jsonConnection.ToObject<JProperty>()?.Name;
        //    var roomId = jsonConnection.First.Value<int>();
        //    var direction = (Direction) Enum.Parse(typeof(Direction), directionString, true);

        //    return new Connection.RoomDirectionPair
        //    {
        //        ConnectsToRoom = rooms.FirstOrDefault(kvp => kvp.Key == jsonConnection.First.Value<int>()).Value,
        //        DoorLocation = direction
        //    };
        //}

        /// <summary>
        /// Creates a room item without items or doors
        /// </summary>
        /// <param name="jsonRoom">JSON string containing the room</param>
        /// <returns>ConnectsToRoom without doors or items</returns>
        private static Room CreateRoom(JToken jsonRoom)
        {
            return new Room
            {
                Id = jsonRoom["id"].Value<int>(),
                Height = jsonRoom["height"].Value<int>(),
                Width = jsonRoom["width"].Value<int>(),
                Connections = new Dictionary<Direction, IDoor>(),
            };
        }

        private List<IItem> CreateItems(IEnumerable<JToken> jsonItems)
        {
            return jsonItems.Select(CreateItem).ToList();
        }

        /// <summary>
        /// Parses a JSON string containing an item. This method will check the item against all item types stored in the ItemTypes dictionary
        /// </summary>
        /// <param name="jsonItem">JSON string containing the item</param>
        /// <returns>Parsed item</returns>
        private IItem CreateItem(JToken jsonItem)
        {
            var type = jsonItem["type"].Value<string>();
            // Check if item type is valid
            var typeKvp = ItemTypes.FirstOrDefault(it => it.Key == type);
            if (typeKvp.Value == null) throw new NoNullAllowedException("Item type " + type + " is not a valid item type.");
            // Parse item and return
            return typeKvp.Value(jsonItem);
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
            return new Trap
            {
                Damage = jsonTrap["damage"].Value<int>(),
                Coordinate = GetItemCoordinates(jsonTrap),
            };
        }

        private static SingleUseTrap CreateSingleUseTrap(JToken jsonTrap)
        {
            return new SingleUseTrap
            {
                Coordinate = GetItemCoordinates(jsonTrap),
                Damage = jsonTrap["damage"].Value<int>(),
            };
        }

        private static SankaraStone CreateSankaraStone(JToken jsonStone)
        {
            return new SankaraStone
            {
                Coordinate = GetItemCoordinates(jsonStone),
            };
        }

        private static Key CreateKey(JToken jsonKey)
        {
            return new Key
            {
                Coordinate = GetItemCoordinates(jsonKey),
                ColorCode = ParseColorString(jsonKey["color"].Value<string>())
            };
        }

        private static PressurePlate CreatePressurePlate(JToken jsonPlate)
        {
            return new PressurePlate()
            {
                Coordinate = GetItemCoordinates(jsonPlate)
            };
        }

        private static Color ParseColorString(string color)
        {
            return Color.FromName(color);
        }
        #endregion
    }
}
