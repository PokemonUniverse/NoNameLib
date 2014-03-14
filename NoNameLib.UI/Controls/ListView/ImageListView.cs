using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NoNameLib.UI.Controls.ListView
{
    [ToolboxItem(true)]
    public class ImageListView : System.Windows.Forms.ListView
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int LV_VIEW_TILE = 0x0004;
        private const int LVM_SETVIEW = 0x108E;

        private ImageList largeImageList;

        public ImageListView()
        {
            View = View.Tile;
            TileSize = new System.Drawing.Size(32, 32);
            LargeImageList = CreateLargeImageList();

            SendMessage(Handle, LVM_SETVIEW, LV_VIEW_TILE, 0);
        }

        private ImageList CreateLargeImageList()
        {
            this.largeImageList = new ImageList();
            this.largeImageList.ColorDepth = ColorDepth.Depth24Bit;
            this.largeImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.largeImageList.TransparentColor = System.Drawing.Color.FromArgb(0, 136, 255);

            return largeImageList;
        }
    }
}