using CODE_GameLib;
using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class PortalFactory
    {
        public static IDoor CreatePortal(JToken jsonPortal)
        {
            return new Portal
            {
                Coordinate = new Coordinate(jsonPortal.Value<int>("x"), jsonPortal.Value<int>("y"))
            };
        }
    }
}