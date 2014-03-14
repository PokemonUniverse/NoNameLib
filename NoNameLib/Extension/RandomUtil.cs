using System;
using System.Globalization;

namespace NoNameLib.Extension
{
    public class RandomUtil
    {
        #region Fields

        private static Random randomClassInstance;
        private static double storedUniformDeviate;
        private static bool storedUniformDeviateIsGood;

        #endregion

        /// <summary>
        /// Get a random number
        /// </summary>
        /// <returns>number</returns>
        public static int GetRandomNumber(int maxValue)
        {
            return randomClassInstance.Next(maxValue);
        }

        /// <summary>
        /// Get a random lowercase character
        /// </summary>
        /// <returns>Random lowercase character</returns>
        public static char GetRandomLowerCaseCharacter()
        {
            return ((char)((short)'a' + randomClassInstance.Next(26)));
        }

        /// <summary>
        /// Get a random uppercase character
        /// </summary>
        /// <returns>Random uppercase character</returns>
        public static char GetRandomUpperCaseCharacter()
        {
            return ((char)((short)'A' + randomClassInstance.Next(26)));
        }

        /// <summary>
        /// Get a string of random lower case letters        
        /// </summary>
        /// <param name="length">Lenght of the string to generate</param>
        /// <returns>Random string</returns>
        public static string GetRandomNumberString(int length)
        {
            string toReturn = "";

            for (int i = 0; i < length; i++)
            {
                toReturn += GetRandomNumber(9).ToString(CultureInfo.InvariantCulture);
            }

            return toReturn;
        }

        /// <summary>
        /// Get a string of random lower case letters        
        /// </summary>
        /// <param name="length">Lenght of the string to generate</param>
        /// <returns>Random string</returns>
        public static string GetRandomLowerCaseString(int length)
        {
            string toReturn = "";

            for (int i = 0; i < length; i++)
            {
                toReturn += GetRandomLowerCaseCharacter();
            }

            return toReturn;
        }

        #region Construction/Initialization

        static RandomUtil()
        {
            Reset();
        }

        /// <summary>
        /// Reset the Random class instance
        /// </summary>
        public static void Reset()
        {
            randomClassInstance = new Random(Environment.TickCount);
        }

        #endregion

        #region Uniform Deviates

        /// <summary>
        /// Returns double in the range [0, 1)
        /// </summary>
        public static double Next()
        {
            return randomClassInstance.NextDouble();
        }

        /// <summary>
        /// Get a random int
        /// </summary>
        /// <param name="minValue">Min</param>
        /// <param name="maxValue">Max</param>
        /// <returns>Int</returns>
        public static int NextInt(int minValue, int maxValue)
        {
            return randomClassInstance.Next(minValue, maxValue);
        }

        /// <summary>
        /// Returns true or false randomly.
        /// </summary>
        public static bool NextBoolean()
        {
            return (randomClassInstance.Next(0, 2) != 0);
        }

        /// <summary>
        /// Returns double in the range [0, 1)
        /// </summary>
        public static double NextDouble()
        {
            double rn = randomClassInstance.NextDouble();
            return rn;
        }

        /// <summary>
        /// Returns Int16 in the range [min, max)
        /// </summary>
        public static Int16 Next(Int16 min, Int16 max)
        {
            if (max <= min)
            {
                const string message = "Max must be greater than min.";
                throw new ArgumentException(message);
            }
            double rn = (max * 1.0 - min * 1.0) * randomClassInstance.NextDouble() + min * 1.0;
            return Convert.ToInt16(rn);
        }

        /// <summary>
        /// Returns Int32 in the range [min, max)
        /// </summary>
        public static int Next(int min, int max)
        {
            return randomClassInstance.Next(min, max);
        }

        /// <summary>
        /// Returns Int64 in the range [min, max)
        /// </summary>
        public static Int64 Next(Int64 min, Int64 max)
        {
            if (max <= min)
            {
                throw new ArgumentException("Max must be greater than min.");
            }

            double rn = (max * 1.0 - min * 1.0) * randomClassInstance.NextDouble() + min * 1.0;
            return Convert.ToInt64(rn);
        }

        /// <summary>
        /// Returns float (Single) in the range [min, max)
        /// </summary>
        public static Single Next(Single min, Single max)
        {
            if (max <= min)
            {
                throw new ArgumentException("Max must be greater than min.");
            }

            double rn = (max * 1.0 - min * 1.0) * randomClassInstance.NextDouble() + min * 1.0;
            return Convert.ToSingle(rn);
        }

        /// <summary>
        /// Returns double in the range [min, max)
        /// </summary>
        public static double Next(double min, double max)
        {
            if (max <= min)
            {
                throw new ArgumentException("Max must be greater than min.");
            }

            double rn = (max - min) * randomClassInstance.NextDouble() + min;
            return rn;
        }

        /// <summary>
        /// Returns DateTime in the range [min, max)
        /// </summary>
        public static DateTime Next(DateTime min, DateTime max)
        {
            if (max <= min)
            {
                throw new ArgumentException("Max must be greater than min.");
            }
            long minTicks = min.Ticks;
            long maxTicks = max.Ticks;
            double rn = (Convert.ToDouble(maxTicks)
               - Convert.ToDouble(minTicks)) * randomClassInstance.NextDouble()
               + Convert.ToDouble(minTicks);
            return new DateTime(Convert.ToInt64(rn));
        }

        /// <summary>
        /// Returns TimeSpan in the range [min, max)
        /// </summary>
        public static TimeSpan Next(TimeSpan min, TimeSpan max)
        {
            if (max <= min)
            {
                throw new ArgumentException("Max must be greater than min.");
            }

            long minTicks = min.Ticks;
            long maxTicks = max.Ticks;
            double rn = (Convert.ToDouble(maxTicks)
               - Convert.ToDouble(minTicks)) * randomClassInstance.NextDouble()
               + Convert.ToDouble(minTicks);
            return new TimeSpan(Convert.ToInt64(rn));
        }

        /// <summary>
        /// Returns double
        /// </summary>
        public static double NextUniform()
        {
            return Next();
        }

        /// <summary>
        /// Returns a uniformly random integer representing one of the values 
        /// in the enum.
        /// </summary>
        public static int NextEnum(Type enumType)
        {
            var values = (int[])Enum.GetValues(enumType);
            int randomIndex = Next(0, values.Length);
            return values[randomIndex];
        }

        #endregion

        #region Exponential Deviates

        /// <summary>
        /// Returns an exponentially distributed, positive, random deviate 
        /// of unit mean.
        /// </summary>
        public static double NextExponential()
        {
            double dum = 0.0;
            while (dum.Equals(0.0))
                dum = NextUniform();
            return -1.0 * Math.Log(dum, Math.E);
        }

        #endregion

        #region Normal Deviates

        /// <summary>
        /// Returns a normally distributed deviate with zero mean and unit 
        /// variance.
        /// </summary>
        public static double NextNormal()
        {
            // based on algorithm from Numerical Recipes
            if (storedUniformDeviateIsGood)
            {
                storedUniformDeviateIsGood = false;
                return storedUniformDeviate;
            }

            double rsq = 0.0;
            double v1 = 0.0, v2 = 0.0;
            while (rsq >= 1.0 || rsq.Equals(0.0))
            {
                v1 = 2.0*Next() - 1.0;
                v2 = 2.0*Next() - 1.0;
                rsq = v1*v1 + v2*v2;
            }
            double fac = Math.Sqrt(-2.0*Math.Log(rsq, Math.E)/rsq);
            storedUniformDeviate = v1*fac;
            storedUniformDeviateIsGood = true;

            return v2*fac;
        }

        #endregion
    }
}
