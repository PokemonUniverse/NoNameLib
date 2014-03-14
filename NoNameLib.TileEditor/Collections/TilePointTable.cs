using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace NoNameLib.TileEditor.Collections
{
    public class TilePointTable : Dictionary<Int64, TilePoint>
    {
        private readonly ReaderWriterLockSlim tableLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public TilePointTable()
        {
        }

        public TilePointTable(IDictionary<Int64, TilePoint> d)
            : base(d)
        {
        }

        public void AddTilePoint(TilePoint tp)
        {
            tableLock.EnterWriteLock(); // LOCK

            Int64 key = GenerateKey(tp.X, tp.Y);
            if (!ContainsKey(key))
            {
                tp.IsNew = false;
                Add(key, tp);
            }
            else
            {
                tp.IsNew = false;
                this[key] = tp;
            }

            tableLock.ExitWriteLock(); // UNLOCK
        }

        public void AddTilePoints(IEnumerable<TilePoint> tilePoints)
        {
            var oldTilePoints = new List<TilePoint>();
            foreach (TilePoint tp in tilePoints)
            {
                Int64 key = GenerateKey(tp.X, tp.Y);
                if (ContainsKey(key))
                {
                    oldTilePoints.Add(this[key]);
                }
                AddTilePoint(tp);
            }
        }

        public void RemoveTilePoint(TilePoint tp)
        {
            Int64 key = GenerateKey(tp.X, tp.Y);
            if (ContainsKey(key))
            {
                tableLock.EnterWriteLock();

                Remove(key);

                tableLock.ExitWriteLock();
            }
        }

        public void RemoveTilePoints(IEnumerable<TilePoint> tilePoints)
        {
            foreach (TilePoint tp in tilePoints)
            {
                RemoveTilePoint(tp);
            }
        }

        /// <summary>
        /// Gets a TilePoint object from TilePointTable using x, y coordinates. Return null if there is no TilePoint and the createNew parameter is false, otherwise a new TilePoint object will be created and returned.
        /// </summary>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordiante</param>
        /// <param name="createNew">Optional - Create new TilePoint if it doesnt exists, default value is true</param>
        /// <returns>TilePoint object if there is a TilePoint at the supplied coordinates or the createNew boolean is true, otherwise NULL.</returns>
        public TilePoint GetTilePoint(int x, int y, bool createNew = true)
        {
            TilePoint tp = GetTilePoint(GenerateKey(x, y), createNew);

            if (tp != null && tp.IsNew)
            {
                tp.X = x;
                tp.Y = y;
            }

            return tp;
        }

        private TilePoint GetTilePoint(Int64 key, bool createNew = true)
        {
            tableLock.EnterReadLock();

            TilePoint tp = null;

            if (ContainsKey(key))
            {
                tp = this[key];
                tp.IsNew = false;
            }
            else if (createNew)
            {
                tp = new TilePoint { IsNew = true };
            }

            tableLock.ExitReadLock();

            return tp;
        }

        public void Merge(Hashtable mapDataTable)
        {
            tableLock.EnterWriteLock();

            foreach (TilePoint tp in mapDataTable.Values)
            {
                var key = GenerateKey(tp.X, tp.Y);
                if (ContainsKey(key))
                {
                    this[key] = tp;
                }
                else
                {
                    Add(key, tp);
                }
            }

            tableLock.ExitWriteLock();
        }

        public static Int64 GenerateKey(int x, int y)
        {
            long x64;
            if (x < 0)
            {
                x64 = (((long)1) << 50) | (~(((long)x) - 1) << 34);
            }
            else
            {
                x64 = (((long)x) << 34);
            }

            long y64;
            if (y < 0)
            {
                y64 = (((long)1) << 33) | (~(((long)y) - 1) << 17);
            }
            else
            {
                y64 = (((long)y) << 17);
            }

            //long z64 = 0; // Z
            var index = (x64 | y64 | 0);

            return index;
        }
    }
}
