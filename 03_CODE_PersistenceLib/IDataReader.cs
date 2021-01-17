using System.Collections.Generic;
using CODE_GameLib;
using CODE_GameLib.Services;

namespace CODE_PersistenceLib
{
    public interface IDataReader
    {
        public Game Read(string filePath);
    }
}
