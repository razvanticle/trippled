namespace TrippleD.Core
{
    public interface IStrategy<in T>
    {
        void Execute(T input);
    }
}