using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CrazyMaze.Game;

namespace CrazyMaze.RenderEntities
{
    public class IRenderEntity
    {
        public GameManager Manager { get; set; }

        public Bitmap Texture { get; set; }

        public Size Size { get; set; }
        public Point Position { get; set; }

        public IRenderEntity(int x, int y, int w, int h)
        {
            Size = new Size(w, h);
            Position = new Point(x, y);
        }

        public virtual void Render(Graphics g)
        {
            if (Texture == null) return;

            lock (GameManager.SYNCHRONIZATION_OBJECT)
                g.DrawImage(Texture, new RectangleF(Position, Size));
        }
        public virtual void Update()
        {

        }
    }
}
