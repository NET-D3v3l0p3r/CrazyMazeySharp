using CrazyMaze.RenderEntities.Tile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyMaze.RenderEntities
{
    public class AdditionalContent : IRenderEntity
    {
        public static int CONTENT_KEY_INCREMENT { get; set; }

        public int ContentKey { get; private set; }
        public ITile Container { get; private set; }

        public AdditionalContent()
            : base(0, 0, 0, 0)
        {
            ContentKey = CONTENT_KEY_INCREMENT++;
            if (!File.Exists(@"textures\treasure\" + ContentKey + ".png"))
                return;

            Texture = new Bitmap(@"textures\treasure\" + ContentKey + ".png");
            Texture.MakeTransparent();
        }   

        public void Bind(ITile container)
        {
            Container = container;
            this.Position = new Point(container.Position.X + (container.Size.Width / 2) - (container.Size.Width / 4), container.Position.Y + (container.Size.Height / 2) - (container.Size.Height / 4));
            this.Size = new Size(container.Size.Width / 2, container.Size.Height / 2);
        }

    }
}
