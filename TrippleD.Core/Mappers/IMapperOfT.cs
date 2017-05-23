namespace TrippleD.Core.Mappers
{
    public interface IMapper<in TIn, out TOut>
    {
        TOut Map(TIn input);
    }
}