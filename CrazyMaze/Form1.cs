using CrazyMaze.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazyMaze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gameManager.Render(e.Graphics);
        }

        GameManager gameManager;

        private void Form1_Load(object sender, EventArgs e)
        {           
            this.DoubleBuffered = true;
            gameManager = new GameManager(12, 12, 50, 1, 1);

            ManualResetEvent sync = new ManualResetEvent(false);
            new Thread(new ThreadStart(() =>
            {
                while (this.Visible)
                {
                    gameManager.Update();
                    this.Invalidate();
                    sync.WaitOne(1);
                    
                }
            })).Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameManager.KEY_PRESSED[(int)e.KeyCode] = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameManager.KEY_PRESSED[(int)e.KeyCode] = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                GameManager.MOUSE_PRESSED[0] = true;
            if (e.Button == MouseButtons.Right)
                GameManager.MOUSE_PRESSED[1] = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                GameManager.MOUSE_PRESSED[0] = false;
            if (e.Button == MouseButtons.Right)
                GameManager.MOUSE_PRESSED[1] = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            GameManager.CURSOR_POSITION = new Point(e.X, e.Y);
        }
    }
}
