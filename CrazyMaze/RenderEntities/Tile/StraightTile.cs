using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyMaze.RenderEntities.Tile
{
    public class StraightTile : ITile
    {
        public StraightTile(int x, int y, int w, int h)
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
                    Directions = new bool[] { false, false, true, true };
                    break;

                case 1:

                    Directions = new bool[] { true, true, false, false };
                    Rotation = -1;
                    break;
            }
        }


    }
}
