using System;

namespace TrippleD.Core.Extensions
{
    public static class FunctionalExtensions
    {
        public static T Execute<T>(this T instance, IStrategy<T> strategy)
        {
            return Tee(instance, strategy.Execute);
        }

        public static TResult Execute<TInput, TResult>(this TInput input, Func<TInput, TResult> func)
        {
            return func(input);
        }

        public static T Tee<T>(this T instance, Action<T> action)
        {
            action(instance);
            return instance;
        }
    }
}