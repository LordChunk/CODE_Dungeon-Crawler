using CODE_GameLib;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CODE_GameLib.Tiles;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        private Dictionary<int, Room> _rooms;

        public Game Read(string filePath)
        {
            var returnGame = new Game();

            var json = JObject.Parse(File.ReadAllText(filePath));
            var jsonRooms = json["rooms"];

            // Parse rooms
            _rooms = new Dictionary<int, Room>();
            if (jsonRooms == null) throw new NoNullAllowedException("This level contains no rooms.");
            foreach (var jsonRoom in jsonRooms)
            {
                if (jsonRoom["type"]?.ToString() != "room") continue;
                var room = CreateRoom(jsonRoom);


                _rooms.Add(room.Id, room);
            }

            // Parse doors/connections
            var jsonConnections = json["connections"].ToList();

            // Create doors and add them to rooms
            jsonConnections.ForEach(jtoken =>
            {
                var type = jtoken.Children<JProperty>().First().Name;
                if (type != "portal")
                    CreateDoorSet(jtoken);
                else
                    CreatePortalSet(jtoken);
            });

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
        /// Creates a room item doors
        /// </summary>
        /// <param name="jsonRoom">JSON string containing the room</param>
        /// <returns>ConnectsToRoom without doors or items</returns>
        private static Room CreateRoom(JToken jsonRoom)
        {
            var items = new List<IItem>();
            var enemies = new List<Enemy>();
            var belts = new Dictionary<Coordinate, ConveyorBelt>();

            var jsonItems = jsonRoom["items"];
            if (jsonItems != null)
                items = ItemFactory.CreateItems(jsonItems);

            var jsonEnemies = jsonRoom["enemies"];
            if (jsonEnemies != null)
                enemies = EnemyFactory.CreateEnemies(jsonEnemies);

            var jsonBelts = jsonRoom["specialFloorTiles"];
            if (jsonBelts != null)
                belts = ConveyorBeltFactory.CreateConveyorBelts(jsonBelts);

            return new Room(
                jsonRoom["id"].Value<int>(), 
                jsonRoom["height"].Value<int>(), 
                jsonRoom["width"].Value<int>(), 
                new Dictionary<Coordinate, IDoor>(),
                items,
                enemies,
                belts
                );
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

        private void CreatePortalSet(JToken jsonPortal)
        {
            var portal1Json = jsonPortal.First.First.First;
            var portal2Json = jsonPortal.First.First.First.Next;
            var portal1 = DoorFactory.CreatePortal(portal1Json);
            var portal2 = DoorFactory.CreatePortal(portal2Json);
            
            var room1 = GetRoomFromId(portal1Json.Value<int>("roomId"));
            var room2 = GetRoomFromId(portal2Json.Value<int>("roomId"));

            portal1.IsInRoom = room1;
            portal2.IsInRoom = room2;

            portal1.ConnectsToDoor = portal2;
            portal2.ConnectsToDoor = portal1;

            room1.Connections.Add(portal1.Coordinate, portal1);
            room2.Connections.Add(portal2.Coordinate, portal2);
        }

    }
}
