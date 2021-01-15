using CODE_GameLib;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CODE_GameLib.Doors;
using CODE_GameLib.Services;

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        private Dictionary<int, Room> _rooms;
        private CheatService _cheatService;


        public GameReader(CheatService cheatService)
        {
            _cheatService = cheatService;
        }

        public Game Read(string filePath)
        {
            var returnGame = new Game(_cheatService);

            var json = JObject.Parse(File.ReadAllText(filePath));
            var jsonRooms = json["rooms"];

            // Parse rooms
            _rooms = new Dictionary<int, Room>();
            if (jsonRooms == null) throw new NoNullAllowedException("This level contains no rooms.");
            foreach (var jsonRoom in jsonRooms)
            {
                if (jsonRoom["type"]?.ToString() != "room") continue;
                var room = CreateRoom(jsonRoom);
                var jsonItems = jsonRoom["items"];
                if (jsonItems != null)
                    room.Items = ItemFactory.CreateItems(jsonItems);
               
                var jsonFloorTiles = jsonRoom["specialFloorTiles"];
                if (jsonFloorTiles != null)
                    room.FloorTiles = FloorFactory.CreateFloorTiles(jsonFloorTiles);

                var jsonEnemies = jsonRoom["enemies"];
                if (jsonFloorTiles != null)
                {
                    if (room.Items != null)
                    {
                        foreach (var enemyItem in ItemFactory.CreateItems(jsonEnemies))
                        {
                            room.Items.Add(enemyItem);
                        }
                    }
                    else
                    {
                        room.Items = ItemFactory.CreateItems(jsonEnemies);
                    }
                }

                _rooms.Add(room.Id, room);
            }

            // Parse doors/connections
            var jsonConnections = json["connections"].ToList();

            // Create doors and add them to rooms
            jsonConnections.ForEach(CreateDoorSet);

            var startRoom = _rooms[json["player"]["startRoomId"].Value<int>()];
            var startCoordinate = new Coordinate(json["player"]["startX"].Value<int>(), json["player"]["startY"].Value<int>());
            var startLives = json["player"]["lives"].Value<int>();

            returnGame.Player = new Player(startCoordinate, startRoom, startLives);

            return returnGame;
        }

        private Room GetRoomFromId(int id)
        {
            return _rooms.FirstOrDefault(kvp => kvp.Key == id).Value;
        }

        /// <summary>
        /// Creates a room item without items or doors
        /// </summary>
        /// <param name="jsonRoom">JSON string containing the room</param>
        /// <returns>ConnectsToRoom without doors or items</returns>
        private static Room CreateRoom(JToken jsonRoom)
        {
            return new Room(jsonRoom["id"].Value<int>(), jsonRoom["height"].Value<int>(), jsonRoom["width"].Value<int>(), new Dictionary<Coordinate, IDoor>());
        }


        /// <summary>
        /// Creates Door instances and links them to each other and to their respective room
        /// </summary>
        /// <param name="jsonConnection">JSON string containing all connections</param>
        private void CreateDoorSet(JToken jsonConnection)
        {
            var door1 = DoorFactory.CreateDoor(jsonConnection);
            var door2 = DoorFactory.CreateDoor(jsonConnection);

            // Connect doors to each other
            door1.ConnectsToDoor = door2;
            door2.ConnectsToDoor = door1;

            var connection1 = jsonConnection.First;
            var connection2 = jsonConnection.First.Next;

            if (door1.GetType() == typeof(Ladder))
            {
                var room1 = GetRoomFromId((int)connection1.First);
                var room2 = GetRoomFromId((int)connection2.First);

                door1.Coordinate = new Coordinate(jsonConnection["ladder"].First.ToObject<int>(), jsonConnection["ladder"].First.Next.ToObject<int>());
                door2.Coordinate = new Coordinate(jsonConnection["ladder"].Last.Previous.ToObject<int>(), jsonConnection["ladder"].Last.ToObject<int>());

                door1.IsInRoom = room1;
                door2.IsInRoom = room2;

                room1.Connections.Add(door1.Coordinate, door1);
                room2.Connections.Add(door2.Coordinate, door2);
            }
            else
            {
                // Parse definitions for first JSON line
                var locationStringDoor2 = connection1.ToObject<JProperty>()?.Name;
                var room1 = GetRoomFromId((int)connection1.First);
                var location2 = (Direction)Enum.Parse(typeof(Direction), locationStringDoor2, true);

                // Parse definitions for 2nd JSON line
                var locationStringDoor1 = connection2.ToObject<JProperty>()?.Name;
                var room2 = GetRoomFromId((int)connection2.First);
                var location1 = (Direction)Enum.Parse(typeof(Direction), locationStringDoor1, true);

                door1.IsInRoom = room1;
                door1.Coordinate = DoorFactory.CalculateDoorCoordinate(door1, location1);

                door2.IsInRoom = room2;
                door2.Coordinate = DoorFactory.CalculateDoorCoordinate(door2, location2);

                room1.Connections.Add(door1.Coordinate, door1);
                room2.Connections.Add(door2.Coordinate, door2);
            }
        }
    }
}
