using CODE_GameLib;
// ReSharper disable RedundantOverriddenMember

namespace CODE_PersistenceLib
{
    public class GameReaderAdapter : GameReader
    {
        public override Game Read(string filePath)
        {
            /*
             * Lees XML bestand uit
             * Converteer XML naar JSON
             */

            return base.Read(filePath);
        }
    }
}

