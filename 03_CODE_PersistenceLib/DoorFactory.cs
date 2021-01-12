using CODE_GameLib.Doors;
using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Enums;

namespace CODE_PersistenceLib
{
    public class DoorFactory
    {
        /// <summary>
        /// A list of all door types and their corresponding parser methods. Each method takes a JToken as it's parameter and outputs an IDoor.
        /// </summary>
        private static readonly Dictionary<string, Func<JToken, IDoor>> _doorTypes = new Dictionary<string, Func<JToken, IDoor>>
        {
            { "colored", CreateColorCodedDoor },
            { "toggle", _ => new ToggleDoor() },
            { "closing gate", _ => new SingleUseDoor() }
        };

        public static IDoor CreateDoor(JToken jsonConnection)
        {
            var jsonDoor = jsonConnection["door"];
            if (jsonDoor == null) return new Door();
            var type = jsonDoor["type"].Value<string>();
            return _doorTypes.FirstOrDefault(kvp => kvp.Key == type).Value(jsonDoor);
        }

        public static Coordinate CalculateDoorCoordinate(IDoor door, Direction direction)
        {
            var width = door.IsInRoom.Width;
            var height = door.IsInRoom.Height;
            var coordinate = new Coordinate(0, 0);

            // calculate location door
            switch (direction)
            {
                case Direction.North:
                    coordinate.X = (width - 1) / 2;
                    coordinate.Y = 0;
                    break;
                case Direction.East:
                    coordinate.X = width - 1;
                    coordinate.Y = (height - 1) / 2;
                    break;
                case Direction.West:
                    coordinate.X = 0;
                    coordinate.Y = (height - 1) / 2;
                    break;
                case Direction.South:
                    coordinate.X = (width - 1) / 2;
                    coordinate.Y = height - 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction");
            }

            return coordinate;
        }

        #region Door create methods

        private static ColorCodedDoor CreateColorCodedDoor(JToken jsonDoor)
        {
            var color = Color.FromName(jsonDoor["color"].Value<string>());

            return new ColorCodedDoor(color);
        }

        #endregion

        public static IDoor CreatePortal(JToken jsonPortal)
        {
            return new Portal
            {
                Coordinate = new Coordinate(jsonPortal.Value<int>("x"), jsonPortal.Value<int>("y"))
            };
        }
    }
}