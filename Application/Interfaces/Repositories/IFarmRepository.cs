
using Application.JSONResponseModel;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IFarmRepository : IGenericRepository<Farm>
    {
        Task<List<WeatherResponse>> Hazard();
    }
}