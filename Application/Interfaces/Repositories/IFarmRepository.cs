using Domain.Entities;
using Persistence.Implementation.Repositories;

namespace Application.Interfaces.Repositories
{
    public interface IFarmRepository : IGenericRepository<Farm>
    {
        Task<List<WeatherResponse>> Hazard();
    }
}