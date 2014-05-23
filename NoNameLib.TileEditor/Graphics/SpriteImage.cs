using System.Drawing;

namespace NoNameLib.TileEditor.Graphics
{
    public class SpriteImage
    {
        #region Properties

        public int Width { get; private set; }
        
        public int Height { get; private set; }

        public Rect TextureCoords { get; set; }

        public int TextureWidth { get; set; }

        public int TextureHeight { get; set; }

        public Point Offset { get; set; }

        #endregion

        public SpriteImage(int width, int height)
        {
            Width = width;
            Height = height;

            Offset = new Point(0, 0);
        }

        public void SetTextureCoords(Rect coords, int textureWidth, int textureHeight)
        {
            TextureCoords = coords;
            TextureHeight = textureHeight;
            TextureWidth = textureWidth;
        }

        public void SetOffset(int x, int y)
        {
            Offset = new Point(x, y);
        }
    }
}
