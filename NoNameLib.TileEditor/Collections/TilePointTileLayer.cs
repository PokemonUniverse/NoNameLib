using NoNameLib.TileEditor.Graphics;

namespace NoNameLib.TileEditor.Collections
{
    public class TilePointTileLayer
    {
        public int Layer { get; set; }

        public int TileId { get; set; }

        public SpriteImage Image { get; set; }

        public TilePointTileLayer(int layer)
        {
            Layer = layer;
        }
    }
}
