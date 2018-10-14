using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using CrazyMaze.RenderEntities.Tile;
using CrazyMaze.Game.Statics;
using CrazyMaze.RenderEntities;

namespace CrazyMaze.Game
{
    public class GameManager
    {
        #region Internal
        public static Random RANDOM = new Random();
        public static object SYNCHRONIZATION_OBJECT = new object();
        #endregion

        #region Input
        public static Point CURSOR_POSITION { get; set; }
        public static bool[] KEY_PRESSED = new bool[255];
        public static bool[] MOUSE_PRESSED = new bool[10];
        #endregion

        #region Definitions
        public static Dictionary<TileType, bool[]> TileTypeConstruction = new Dictionary<TileType, bool[]>()
        {
            { TileType.L, new bool[]{ false, true, true, false } },
            { TileType.I, new bool[]{ false, false, true, true} },
            { TileType.T, new bool[]{ false, true, true, true} },
        };
        public static Dictionary<Type, TileType> ClassTypes = new Dictionary<Type, TileType>()
        {
            { typeof(StraightTile), TileType.I },
            { typeof(CornerTile), TileType.L },
            { typeof(TTile), TileType.T }
        };

        public static HashSet<int> UsedCards = new HashSet<int>();
        #endregion

        #region Enums
        public enum TileType
        {
            T,
            I,
            L
        }
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        };
        #endregion

        #region Gamefield
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int InitX { get; private set; }
        public int InitY { get; private set; }

        public int CellSize { get; private set; }

        public ITile[] Map { get; private set; }
        #endregion

        #region Accessor
        public ITile this[int x, int y]
        {
            get
            {
                if (x + y * Width > Map.Length)
                    return new ITile(0, 0, 0, 0);
                if (x + y * Width < 0)
                    return new ITile(0, 0, 0, 0);

                return Map[x + y * Width];
            }
            set { Map[x + y * Width] = value; }
        }
        #endregion

        #region Private declaration
        private bool isPressedLeft;
        private bool isPressedRight;
        #endregion

        #region Constructor
        public GameManager(int w, int h, int cellSize, int tX, int tY)
        {
            Width = w;
            Height = h;

            InitX = tX;
            InitY = tY;

            CellSize = cellSize;

            Map = new ITile[Width * Height];


            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int num = RANDOM.Next(0, 10);
                    if (num < 4)
                        this[x, y] = new CornerTile(x + InitX, y + InitY, CellSize, CellSize);
                    else if (num > 4 && num < 7) this[x, y] = new StraightTile(x + InitX, y + InitY, CellSize, CellSize);
                    else this[x, y] = new TTile(x + InitX, y + InitY, CellSize, CellSize);
                }
            }

            this[2, 3].AddContent(new AdditionalContent());
            this[6, 2].AddContent(new AdditionalContent());
            this[1, 1].AddContent(new AdditionalContent());
            this[6, 4].AddContent(new AdditionalContent());
            this[3, 3].AddContent(new AdditionalContent());


        }
        #endregion

        #region Logic
        public void Render(Graphics g)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                Map[i]?.Render(g);
            }
        }
        public void Update()
        {
            if (IsMousePressed(MouseButtons.Left) && !isPressedLeft)
            {
                Floodfill.Reset(this);
                Floodfill.FloodFill(this, CURSOR_POSITION.X / CellSize - InitX, CURSOR_POSITION.Y / CellSize - InitY);
                isPressedLeft = true;
            }
            if (IsMouseReleased(MouseButtons.Left))
                isPressedLeft = false;


            if(IsMousePressed(MouseButtons.Right) && !isPressedRight)
            {
                isPressedRight = true;
                this[CURSOR_POSITION.X / CellSize - InitX, CURSOR_POSITION.Y / CellSize - InitY].Rotate();
            }
            if (IsMouseReleased(MouseButtons.Right))
                isPressedRight = false;


        }
        #endregion

        #region InputManager
        public static bool IsKeyPressed(Keys key)
        {
            return KEY_PRESSED[(int)key];
        }
        public static bool IsKeyReleased(Keys key)
        {
            return !KEY_PRESSED[(int)key];
        }

        public static bool IsMousePressed(MouseButtons mouseButton)
        {
            if (mouseButton == MouseButtons.Left)
                return GameManager.MOUSE_PRESSED[0];
            if (mouseButton == MouseButtons.Right)
                return GameManager.MOUSE_PRESSED[1];
            return false;
        }
        public static bool IsMouseReleased(MouseButtons mouseButton)
        {
            return !IsMousePressed(mouseButton);
        }
        #endregion


    }
}
