using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NoNameLib.Extension;

namespace NoNameLib.UI.Controls.ImageView
{
    public partial class TilesetView : UserControl
    {
        private string tilesetPath = "";

        private bool isMouseDown;
        private bool hasMouseMoved;
        private Point startPointMouseDown;
        private Point lastPointMouseMove = new Point(0, 0);

        #region Properties

        [Browsable(true)]
        public int TileSize { get; set; }

        [Browsable(true)]
        public string TilesetPath
        {
            get { return tilesetPath; }
            set
            {
                tilesetPath = value;
                LoadImage(tilesetPath);
            }
        }

        public int[][] SelectedSprites { get; private set; }

        #endregion

        public TilesetView()
        {
            InitializeComponent();

            TileSize = 32;
            SelectedSprites = new[] {new int[0]};
        }

        private void LoadImage(string path)
        {
            if (!File.Exists(path))
                return;

            pictureBoxMain.Image = new Bitmap(path);
        }

        private void pictureBoxMain_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            int x = (e.Location.X / 32);
            int y = (e.Location.Y / 32);

            startPointMouseDown = new Point(x, y);

            hasMouseMoved = false;
            isMouseDown = true;
        }

        private void pictureBoxMain_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown)
                return;

            int x = e.Location.X / TileSize;
            int y = e.Location.Y / TileSize;

            if (lastPointMouseMove.X == x && lastPointMouseMove.Y == y)
                return;

            hasMouseMoved = true;

            lastPointMouseMove.X = x;
            lastPointMouseMove.Y = y;

            var diffX = lastPointMouseMove.X - startPointMouseDown.X;
            var diffY = lastPointMouseMove.Y - startPointMouseDown.Y;

            DrawOverlay(startPointMouseDown.X, startPointMouseDown.Y, diffX, diffY);
        }

        private void pictureBoxMain_MouseUp(object sender, MouseEventArgs e)
        {
            int x = e.Location.X / TileSize;
            int y = e.Location.Y / TileSize;

            if (!hasMouseMoved)
            {
                DrawOverlay(x, y, 1, 1);
            }

            lastPointMouseMove.X = 0;
            lastPointMouseMove.Y = 0;

            isMouseDown = false;
        }

        private void DrawOverlay(int startX, int startY, int diffX, int diffY)
        {
            if (TilesetPath.IsNullOrWhiteSpace())
                return;

            if (diffX < 0)
            {
                startX += diffX;
                diffX *= -1;
            }

            if (diffY < 0)
            {
                startY += diffY;
                diffY *= -1;
            }

            var baseImage = new Bitmap(TilesetPath);
            using (Graphics g = Graphics.FromImage(baseImage))
            {
                var customColor = Color.FromArgb(125, Color.Blue);
                var shadowBrush = new SolidBrush(customColor);
                g.FillRectangles(shadowBrush, new[] { new RectangleF(startX * TileSize, startY * TileSize, (diffX + 1) * TileSize, (diffY + 1) * TileSize) });
            }
            pictureBoxMain.Image = baseImage;
            Application.DoEvents();
        }
    }
}
