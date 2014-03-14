using System;
using System.Collections.Generic;

namespace NoNameLib.TileEditor.Collections
{
    public class TilePoint
    {
        private Int64? index;

        #region Ctor

        public TilePoint()
        {
            Layers = new SortedDictionary<int, TilePointLayer>();
        }

        public TilePoint(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set the X position
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Get or set the Y position
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Read only int64 index
        /// </summary>
        public Int64 Index
        {
            get
            {
                if (!index.HasValue)
                    index = TilePointTable.GenerateKey(X, Y);

                return index.Value;
            }
        }

        /// <summary>
        /// Gets a boolean if this TilePoint is newly created
        /// </summary>
        public bool IsNew { get; internal set; }

        /// <summary>
        /// Gets a list of TilePointLayers linked to this TilePoint
        /// </summary>
        public SortedDictionary<int, TilePointLayer> Layers { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a TilePointLayer based on the layerIndex
        /// </summary>
        /// <param name="zindex">Layer index to get</param>
        /// <param name="createNew">Whether the tilepoint layer should be created if it doesnt exists</param>
        /// <returns>TilePointLayer object or null</returns>
        public TilePointLayer GetLayer(int zindex, bool createNew = false)
        {
            TilePointLayer tilePointLayer;
            if (!Layers.TryGetValue(zindex, out tilePointLayer) && createNew)
            {
                tilePointLayer = new TilePointLayer(this, zindex);
                Layers[zindex] = tilePointLayer;
            }

            return tilePointLayer;
        }

        /// <summary>
        /// Adds/Updates a TilePointLayer on this TilePoint
        /// </summary>
        /// <param name="tilePointLayer">TilePointLayer object to add</param>
        public void SetLayer(TilePointLayer tilePointLayer)
        {
            Layers[tilePointLayer.Z] = tilePointLayer;
        }

        /// <summary>
        /// Checks if tile TilePoint has a layer on supplied Z-index
        /// </summary>
        /// <param name="zindex">Z-index layer to check</param>
        /// <returns>True if layer exists, otherwise false</returns>
        public bool HasLayer(int zindex)
        {
            TilePointLayer outLayer;
            return Layers.TryGetValue(zindex, out outLayer);
        }

        /// <summary>
        /// Get the string representation of this TilePoint
        /// </summary>
        /// <returns>X, Y and Z values as string</returns>
        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}", X, Y);
        }

        #endregion

        #region Operators

        public bool Equals(TilePoint tp)
        {
            if (ReferenceEquals(null, tp)) return false;
            
            return X == tp.X && Y == tp.Y;
        }

        public static bool operator ==(TilePoint a, TilePoint b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(TilePoint a, TilePoint b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TilePoint)obj);
        }

        /// <summary>
        /// DO NOT USE UNLESS YOU'RE ABSOLUTELY SURE YOU NEED THIS AND YOU KNOW WHAT THE FUCK YOU'RE DOING, OTHERWISE JUST USE THE `Index` VARIABLE.
        /// This method is just here so the compiler doesn't give crap about non-implemented members.
        /// </summary>
        /// <returns>An integer, duh</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Creates a new TilePoint with the X, Y difference of tp1 - tp2
        /// </summary>
        /// <param name="tp1">TilePoint 1</param>
        /// <param name="tp2">TilePoint 2</param>
        /// <returns></returns>
        public static TilePoint Difference(TilePoint tp1, TilePoint tp2)
        {
            var newTilePoint = new TilePoint
            {
                X = tp1.X - tp2.X,
                Y = tp1.Y - tp2.Y
            };
            return newTilePoint;
        }

        #endregion
    }
}