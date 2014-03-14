namespace NoNameLib.TileEditor.Collections
{
    public class TilePointTileLayer
    {
        public int Layer { get; set; }

        public string TileId { get; set; }

        public TilePointTileLayer(int layer)
        {
            Layer = layer;
        }
    }
}
