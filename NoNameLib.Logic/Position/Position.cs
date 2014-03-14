using System;

namespace NoNameLib.Logic.Position
{
    public class Position
    {
        private int z;

        public Position()
            : this(0,0,0)
        {
        }

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Position(long hash)
        {
            Z = ((short)hash);

            var y64 = (hash >> 17) & 0xFFFF;
            var yabs = (byte)((hash >> 33) & 0x01);
            var x64 = (hash >> 34) & 0xFFFF;
            var xabs = (hash >> 50) & 0x01;

            Y = (int)y64;
            if (yabs == 1)
                Y = 0 - Y;

            X = (int)x64;
            if (xabs == 1)
                X = 0 - X;
        }

        #region Properties

        public int X { get; set; }

        public int Y { get; set; }

        public int Z
        {
            get { return z; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "Value has to be equal or greater than 0");
                z = value;
            }
        }

        #endregion

        #region Methods

        public long GetHash()
        {
            long x64;
            if (X < 0)
            {
                x64 = (((long)1) << 50) | (~(((long)X) - 1) << 34);
            }
            else
            {
                x64 = (((long)X) << 34);
            }

            long y64;
            if (Y < 0)
            {
                y64 = (((long)1) << 33) | (~(((long)Y) - 1) << 17);
            }
            else
            {
                y64 = (((long)Y) << 17);
            }

            long z64 = Z;
            long index = (x64 | y64 | z64);

            return index;
        }

        /// <summary>
        /// Returns a new position object with the X and Y values added
        /// </summary>
        /// <param name="p">Position object which will be added to the current</param>
        /// <returns>New Position object with the new values</returns>
        public Position Add(Position p)
        {
            return new Position(this.X + p.X, this.Y + p.Y, 0);
        }

        /// <summary>
        /// Returns a new position object with the X and Y values substracted
        /// </summary>
        /// <param name="p">Position object which will be substracted from the current</param>
        /// <returns>New Position object with the new values</returns>
        public Position Substract(Position p)
        {
            return new Position(this.X - p.X, this.Y - p.Y, 0);
        }

        /// <summary>
        /// Compares this Position object with the supplied object. Returns true if values are the same, otherwise false
        /// </summary>
        /// <param name="p">Position object to compare with</param>
        /// <returns>Returns true if values are the same, otherwise false</returns>
        public bool Equals(Position p)
        {
            return (this.X == p.X && this.Y == p.Y && this.Z == p.Z);
        }

        /// <summary>
        /// Checks if this position is in range of the supplied position, based on given delta for x, y and z
        /// </summary>
        /// <param name="p">Position to compare with</param>
        /// <param name="deltaX">Max difference for X</param>
        /// <param name="deltaY">Max difference for Y</param>
        /// <param name="deltaZ">Max difference for Z</param>
        /// <returns>True if positions are in range of each other, otherwise false</returns>
        public bool InRange(Position p, int deltaX, int deltaY, int deltaZ = 0)
        {
            var diffX = Math.Abs(X - p.X);
            var diffY = Math.Abs(Y - p.Y);
            var diffZ = Math.Abs(Z - p.Z);

            return (diffX <= deltaX && diffY <= deltaY && diffZ <= deltaZ);
        }

        /// <summary>
        /// Convert position object into readable string with the format X,Y,Z
        /// </summary>
        /// <returns>String representation of set position</returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", X, Y, Z);
        }

        #endregion
    }
}
