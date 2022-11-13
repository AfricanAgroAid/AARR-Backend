
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.GatewayServices;
using Application.JSONResponseModel;
using Application.JSONResponseModel.OpenWeatherMapServiceResponseModels;
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
            var coolHazardous = item.DailyForecasts.Where(x => x.Temperature.Maximum > 34 || x.Temperature.Minimum < 2.5);
            var hotHazardous = item.DailyForecasts.Where(x => x.Temperature.Maximum > 77 || x.Temperature.Minimum < 34);
            if (coolHazardous.Count() > 0)
            {
                var locations = item.City.Name;
                var farms = await GetAllFarmsByLocation(locations);
                foreach (var farm in farms)
                {
                    var weatherResponse = new WeatherResponse
                    {
                        Description = AddDescription(coolHazardous),
                        FarmerPhoneNumber = farm.Farmer.PhoneNumber,
                        FarmLocation = farm.LocatedCity,
                        FarmName = farm.FarmName,
                        DateOfIncidence = coolHazardous.First().Dates.ForecastDate,
                    };
                    weatherResponses.Add(weatherResponse);
                }
            }
            if (hotHazardous.Count() > 0)
            {
                var locations = item.City.Name;
                var farms = await GetAllFarmsByLocation(locations);
                foreach (var farm in farms)
                {
                    var weatherResponse = new WeatherResponse
                    {
                        Description = AddDescription(hotHazardous),
                        FarmerPhoneNumber = farm.Farmer.PhoneNumber,
                        FarmLocation = farm.LocatedCity,
                        FarmName = farm.FarmName,
                        DateOfIncidence = hotHazardous.First().Dates.ForecastDate,
                    };
                    weatherResponses.Add(weatherResponse);
                }
            }
        }
        return weatherResponses;
    }
    private List<string> AddDescription(IEnumerable<DailyForecastInformation> hazardous)
    {
        var weatherInfos = hazardous.Select(x => x.WeatherInformations).ToList();
        List<string> desc = new List<string>();
        foreach (var weatherInfo in weatherInfos)
        {
            foreach (var d in weatherInfo)
            {
                desc.Add(d.Description);
            }
        }
        return desc;
    }
}