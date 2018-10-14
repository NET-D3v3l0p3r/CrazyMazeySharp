using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyMaze.RenderEntities.Tile
{
   public class TTile: ITile
    {
        public TTile(int x, int y, int w, int h)
           : base(x, y, w, h)
        {

        }
        // Left Right Up Down
        public override void Rotate()
        {
            base.Rotate();

            switch (Rotation)
            {
                case 0:
                    Directions = new bool[] { false, true, true, true };
                    break;

                case 1:
                    Directions = new bool[] { true, true, false, true };
                    break;

                case 2:
                    Directions = new bool[] { true, false, true, true };
                    break;

                case 3:
                    Directions = new bool[] { true, true, true, false };
                    Rotation = -1;
                    break;
            }
        }
    }
}
