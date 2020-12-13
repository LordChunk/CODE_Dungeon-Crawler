using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CODE_GameLib.Doors;
using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;
using Newtonsoft.Json.Linq;

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

        #region Door create methods

        private static ColorCodedDoor CreateColorCodedDoor(JToken jsonDoor)
        {
            var color = Color.FromName(jsonDoor["color"].Value<string>());

            return new ColorCodedDoor(color);
        }

        #endregion

    }
}