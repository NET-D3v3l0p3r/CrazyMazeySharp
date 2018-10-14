using CrazyMaze.RenderEntities.Tile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CrazyMaze.Game.GameManager;

namespace CrazyMaze.Game.Statics
{
    public class Floodfill
    {
        static List<Point> PROCESSED_POINTS = new List<Point>();
        public static void Reset(GameManager map)
        {
            for (int i = 0; i < map.Map.Length; i++)
                map.Map[i].IsHitByFlood = false;

            PROCESSED_POINTS.Clear();
        }

        public static void FloodFill(GameManager map, int x, int y)
        {
            if (PROCESSED_POINTS.Contains(new Point(x, y)))
                return;

            ITile c = map[x, y];
            c.IsHitByFlood = true;


            PROCESSED_POINTS.Add(new Point(x, y));

            if (y + 1 < map.Height)
                if (c.Directions[(int)Direction.Down])
                    if (map[x, y + 1].Directions[(int)Direction.Up])
                        FloodFill(map, x, y + 1);

            if (y - 1 >= 0)
                if (c.Directions[(int)Direction.Up])
                    if (map[x, y - 1].Directions[(int)Direction.Down])
                        FloodFill(map, x, y - 1);

            if (x + 1 < map.Width)
                if (c.Directions[(int)Direction.Right])
                    if (map[x + 1, y].Directions[(int)Direction.Left])
                        FloodFill(map, x + 1, y);

            if (x - 1 >= 0)
                if (c.Directions[(int)Direction.Left])
                    if (map[x - 1, y].Directions[(int)Direction.Right])
                        FloodFill(map, x - 1, y);


        }
    }
}