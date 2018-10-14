using CrazyMaze.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyMaze.RenderEntities.Tile
{
    public class ITile : IRenderEntity
    {
        public GameManager.TileType Type { get; set; }
        public AdditionalContent Content { get; private set; }

        public bool IsHitByFlood { get; set; }
        public int Rotation { get; set; }
        public static object Direction { get; internal set; }

        public bool[] Directions = new bool[4];

        public void AddContent(AdditionalContent content)
        {
            Content = content;
            Content.Bind(this);
        }

        public ITile(int x, int y, int w, int h)
            : base(x * w, y * h, w, h)
        {
            Type = GameManager.ClassTypes[this.GetType()];
            Directions = GameManager.TileTypeConstruction[Type];

            Texture = new Bitmap(@"textures\tiles\" + Type + ".png");
        }

        public override void Render(Graphics g)
        {
            base.Render(g);
            Content?.Render(g);
            if (IsHitByFlood)
                g.FillRectangle(Brushes.Blue, this.Position.X + Size.Width / 2 - 5, this.Position.Y + Size.Height / 2 - 5, 10, 10);
        }

        public virtual void Rotate()
        {
            Rotation++;

            Texture.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Content?.Texture.RotateFlip(RotateFlipType.Rotate90FlipNone);

        }
    }
}
