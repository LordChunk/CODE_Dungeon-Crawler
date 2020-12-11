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
        private static Dictionary<string, Func<JToken, IItem>> ItemTypes = new Dictionary<string, Func<JToken, IItem>>
        {
            { "boobietrap", CreateTrap },
            { "disappearing boobietrap", CreateSingleUseTrap },
            { "sankara stone", CreateSankaraStone },
            { "key", CreateKey },
            { "pressure plate", CreatePressurePlate },
        };

        private static Dictionary<string, IConnection> ConnectionTypes = new Dictionary<string, IConnection>
        {
            { "colored", new ColorCodedConnection() },
            { "toggle", new ToggleConnection() },
            { "closing gate", new SingleUseConnection() }
        };


        private Dictionary<int, Room> rooms;

        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));
            var jsonRooms = json["rooms"];
            Room startRoom;

            rooms = new Dictionary<int, Room>();
            // Parse rooms
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
            var jsonConnections = json["connections"];
            var connections = jsonConnections.Select(CreateConnection).ToList();

            // Link connections to rooms
            foreach (var connection in connections)
            {
                var pair0 = connection.Rooms[0];
                var pair1 = connection.Rooms[1];

                pair0.Room.Connections.Add(pair1.DoorLocation, connection);
                pair1.Room.Connections.Add(pair0.DoorLocation, connection);
            }

            return new Game();
        }

        private IConnection CreateConnection(JToken jsonConnection)
        {
            IConnection connection;
            // Check if connection between room is "special connection"
            var jsonDoor = jsonConnection["door"];
            if (jsonDoor != null)
            {
                // Get door type
                var type = jsonDoor["type"].Value<string>();

                var (_, roomConnection) = ConnectionTypes.FirstOrDefault(kvp => kvp.Key == type);
                connection = roomConnection ?? throw new NoNullAllowedException("Connection type " + type + " is not a valid connection type.");
            }
            else
            {
                connection = new Connection();
            }

            // Add two connections to the rooms
            connection.Rooms = new List<Connection.RoomDirectionPair>
            {
                CreateConnectionPair(jsonConnection.First), 
                CreateConnectionPair(jsonConnection.First.Next)
            };

            return connection;
        }

        private Connection.RoomDirectionPair CreateConnectionPair(JToken jsonConnection)
        {
            var directionString = jsonConnection.ToObject<JProperty>()?.Name;
            var roomId = jsonConnection.First.Value<int>();
            var direction = (Direction) Enum.Parse(typeof(Direction), directionString, true);

            return new Connection.RoomDirectionPair
            {
                Room = rooms.FirstOrDefault(kvp => kvp.Key == jsonConnection.First.Value<int>()).Value,
                DoorLocation = direction
            };
        }

        /// <summary>
        /// Creates a room item without items or doors
        /// </summary>
        /// <param name="jsonRoom">JSON string containing the room</param>
        /// <returns>Room without doors or items</returns>
        private static Room CreateRoom(JToken jsonRoom)
        {
            return new Room
            {
                Id = jsonRoom["id"].Value<int>(),
                Height = jsonRoom["height"].Value<int>(),
                Width = jsonRoom["width"].Value<int>(),
                Connections = new Dictionary<Direction, IConnection>(),
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
        private static IItem CreateItem(JToken jsonItem)
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
