using System.Collections.Generic;
using System.Data;
using System.Linq;
using CODE_TempleOfDoom_DownloadableContent;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class EnemyFactory
    {

        public static List<Enemy> CreateEnemies(IEnumerable<JToken> jsonItems)
        {
            return jsonItems.Select(CreateEnemy).ToList();

        }

        public static Enemy CreateEnemy(JToken jtoken)
        {
            var type = jtoken.Value<string>("type");
            var numberOfLives = 3;
            var x = jtoken.Value<int>("x");
            var y = jtoken.Value<int>("y");
            var minX = jtoken.Value<int>("minX");
            var minY = jtoken.Value<int>("minY");
            var maxX = jtoken.Value<int>("maxX");
            var maxY = jtoken.Value<int>("maxY");

            if (type == "horizontal")
                return new HorizontallyMovingEnemy(numberOfLives, x, y, minX, maxX);
            else if (type == "vertical")
                return new VerticallyMovingEnemy(numberOfLives, x, y, minY, maxY);

            throw new NoNullAllowedException("Enemy type " + type + " is not a valid item type.");
        }
    }
}