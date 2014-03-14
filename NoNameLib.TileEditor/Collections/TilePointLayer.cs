using System.Collections.Generic;
using NoNameLib.TileEditor.Enums;

namespace NoNameLib.TileEditor.Collections
{
    public class TilePointLayer
    {
        private readonly TilePoint parent;

        public TilePointLayer(TilePoint tilePointParent, int z)
        {
            parent = tilePointParent;

            TileLayers = new SortedDictionary<int, TilePointTileLayer>();

            Z = z;
            Movement = MovementTypes.Walk;
            Event = null;
        }

        /// <summary>
        /// Get or set the Z index
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Get or set the MovementType
        /// </summary>
        public MovementTypes Movement { get; set; }

        /// <summary>
        /// Dictionary containing all TilePointLayers for this TilePoint
        /// </summary>
        public SortedDictionary<int, TilePointTileLayer> TileLayers { get; private set; }

        /// <summary>
        /// Gets or sets the Event object
        /// </summary>
        public object Event { get; set; }

        #region Public Methods

        /// <summary>
        /// Gets a TilePointLayer based on the layerIndex
        /// </summary>
        /// <param name="layerIndex">Layer index to get</param>
        /// <param name="createNew">Optional boolean, if true a new TIlePointTileLayer will be created when it doesn't exists</param>
        /// <returns>TilePointLayer object or null</returns>
        public TilePointTileLayer GetLayer(int layerIndex, bool createNew = false)
        {
            TilePointTileLayer tilePointLayer;
            if (!TileLayers.TryGetValue(layerIndex, out tilePointLayer) && createNew)
            {
                tilePointLayer = new TilePointTileLayer(layerIndex);
            }
            return tilePointLayer;
        }

        /// <summary>
        /// Adds/Updates a TilePointLayer on this TilePoint
        /// </summary>
        /// <param name="tilePointLayer">TilePointLayer object to add</param>
        public void SetLayer(TilePointTileLayer tilePointLayer)
        {
            TileLayers[tilePointLayer.Layer] = tilePointLayer;
        }

        /// <summary>
        /// Returns boolean whether supplied layer exists or not.
        /// </summary>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        public bool HasLayer(int layerIndex)
        {
            return TileLayers.ContainsKey(layerIndex);
        }

        /// <summary>
        /// Get the string representation of this TilePoint
        /// </summary>
        /// <returns>X, Y and Z values as string</returns>
        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}, Z = {2}", parent.X, parent.Y, Z);
        }

        #endregion
    }
}
