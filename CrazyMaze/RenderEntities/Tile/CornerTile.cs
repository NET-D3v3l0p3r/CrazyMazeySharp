using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyMaze.RenderEntities.Tile
{
    public class CornerTile : ITile
    {
        public CornerTile(int x, int y, int w, int h)
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
                    Directions = new bool[] {false, true, true, false };
                    break;

                case 1:
                    Directions = new bool[] { false, true, false, true };
                    break;

                case 2:
                    Directions = new bool[] { true, false, false, true };
                    break;

                case 3:
                    Directions = new bool[] { true, false, true, false };
                    Rotation = -1;
                    break;
            }
        }
    }
}
