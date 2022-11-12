
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.GatewayServices;
using Application.JSONResponseModel;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Implementation.Repositories;

namespace Persistence.Repositories;

public class FarmRepository : GenericRepository<Farm>, IFarmRepository
{
    private readonly IOpenWeatherMapService _open;
    public FarmRepository(ApplicationContext context, IOpenWeatherMapService open)
    {
        _context = context;
        _open = open;
    }
    public async Task<IReadOnlyList<Farm>> GetAllFarmsByLocation(string location)
    {
        var farms = await _context.Farms.Include(x => x.Farmer).Where(x => x.LocatedCity == location).ToListAsync();
        return farms;
    }
    private async Task<List<string>> GetAllFarmsWithDistinctLocation()
    {
        var farms = await _context.Farms.Select(x => x.LocatedCity).Distinct().ToListAsync();
        return farms;
    }
    public async Task<List<WeatherResponse>> Hazard()
    {
        List<WeatherResponse> weatherResponses = new List<WeatherResponse>();
        var a = await GetAllFarmsWithDistinctLocation();
        var ret = await _open.GetDaysClimaticForecastAsync(a, "metric", 30);
        foreach (var item in ret)
        {
            var hazardous = item.DailyForecasts.Where(x => x.Temperature.Maximum > 9 - 3.5 || x.Temperature.Minimum < 36.5);
            if (hazardous.Count() > 0)
            {
                var locations = item.City.Name;
                var farms = await GetAllFarmsByLocation(locations);
                foreach (var farm in farms)
                {
                    var weatherInfo = hazardous.Select(x => x.WeatherInformations).ToList();
                    List<string> desc = new List<string>();
                    foreach (var describe in weatherInfo)
                    {
                        foreach (var d in describe)
                        {
                            desc.Add(d.Description);
                        }
                    }
                    var weatherResponse = new WeatherResponse
                    {
                        Description = desc,
                        FarmerPhoneNumber = farm.Farmer.PhoneNumber,
                        FarmLocation = farm.LocatedCity,
                        FarmName = farm.FarmName
                    };
                    weatherResponses.Add(weatherResponse);
                }
            }

        }
        return weatherResponses;
    }
}