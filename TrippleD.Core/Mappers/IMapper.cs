namespace TrippleD.Core.Mappers
{
    public interface IMapper
    {
        TOutput Map<TInput, TOutput>(TInput input);
    }
}