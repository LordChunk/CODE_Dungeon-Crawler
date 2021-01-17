using CODE_GameLib;
using System;
using System.Collections.Generic;
using System.Text;
using CODE_GameLib.Doors;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Services;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class SqlGameReader : IDataReader
    {
        private Dictionary<int, Room> _rooms;
        private readonly CheatService _cheatService;

        public SqlGameReader(CheatService cheatService)
        {
            _cheatService = cheatService;
        }

        public Game Read(string filePath)
        {
            throw new NotImplementedException();
        }


        private Room GetRoomFromId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a room item without items or doors
        /// </summary>
        /// <param name="jsonRoom">JSON string containing the room</param>
        /// <returns>ConnectsToRoom without doors or items</returns>
        private static Room CreateRoom(string jsonRoom)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Creates Door instances and links them to each other and to their respective room
        /// </summary>
        /// <param name="jsonConnection">JSON string containing all connections</param>
        private void CreateDoorSet(string jsonConnection)
        {
            throw new NotImplementedException();
        }
    }
}
