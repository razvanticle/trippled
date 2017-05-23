using System;

namespace TrippleD.Core
{
    public static class Disposable
    {
        public static TResult Using<TDisposable, TResult>
        (
            Func<TDisposable> factory,
            Func<TDisposable, TResult> fn)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return fn(disposable);
            }
        }

        public static void Using<TDisposable>
        (
            Func<TDisposable> factory,
            Action<TDisposable> fn)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                fn(disposable);
            }
        }
    }
}