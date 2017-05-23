using System;
using Microsoft.Extensions.DependencyInjection;

namespace TrippleD.Core.Mappers
{
    [Service(typeof(IMapper))]
    public class Mapper : IMapper
    {
        private readonly IServiceProvider serviceProvider;

        public Mapper(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TOutput Map<TInput, TOutput>(TInput input)
        {
            var mapper = serviceProvider.GetService<IMapper<TInput, TOutput>>();

            return mapper.Map(input);
        }
    }
}