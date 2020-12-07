using System.Drawing;
using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Doors
{
    public class ColorCodedDoor : Door, IColorBinder, ILockable
    {
        public Color ColorCode { get; set; }
        public bool IsLocked { get; set; }
    }
}