using System;

namespace TrippleD.Core
{
    public static class Guard
    {
        public static void ArgGreaterThanZero(int value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        public static void ArgNotEmpty<T>(T arg, string argName)
        {
            if (arg.Equals(default(T)))
            {
                throw new ArgumentNullException(argName);
            }
        }
        
        public static void ArgNotNull<T>(T arg, string argName) where T : class
        {
            if (ReferenceEquals(arg, null))
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}