using System;

namespace TrippleD.Core
{
    public static class Guard
    {
        /// <summary>
        ///     Checks if an argument is null. If it is, throws an <see cref="ArgumentNullException" /> with the specified
        ///     <paramref name="argName" />
        /// </summary>
        /// <typeparam name="T">type of the argument, must be a reference type</typeparam>
        /// <param name="arg">argument to check</param>
        /// <param name="argName">name of argument</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgNotNull<T>(T arg, string argName) where T : class
        {
            if (ReferenceEquals(arg, null))
            {
                throw new ArgumentNullException(argName);
            }
        }

        public static void ArgGreaterThanZero(int value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
    }
}