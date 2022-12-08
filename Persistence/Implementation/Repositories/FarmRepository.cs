
using System.Linq.Expressions;
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
    public async Task<IReadOnlyList<Farm>> GetAllFarmsWithFarmer(Expression<Func<Farm, bool>> exp)
    {
        var farms = await _context.Farms.Include(x => x.Farmer).Where(exp).ToListAsync();
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
            var coolHazardous = item.DailyForecasts.Where(x => x.Temperature.Maximum > 34 || x.Temperature.Minimum < 2.5).ToList();
            var hotHazardous = item.DailyForecasts.Where(x => x.Temperature.Maximum > 77 || x.Temperature.Minimum < 34).ToList();
            var location = item.City.Name;
            if (coolHazardous.Count() > 0)
            {
                var farms = await GetAllFarmsWithFarmer(x => x.CropType == Domain.Enums.CropType.RainSeason && x.LocatedCity == location);
                foreach (var info in coolHazardous)
                {
                    if(farms.Count() > 0)
                    {

                        foreach (var farm in farms)
                        {
                            var weatherResponse = new WeatherResponse
                            {
                                Description = info.WeatherInformations[0].Description,
                                FarmerPhoneNumber = farm.Farmer.PhoneNumber,
                                FarmLocation = farm.LocatedCity,
                                FarmName = farm.FarmName,
                                DateOfIncidence = info.Dates.ForecastDate,
                                IncidenceType ="Cool Weather Hazard",
                                FarmerCountryCode = farm.Farmer.CountryPhoneCode
                            };
                            weatherResponses.Add(weatherResponse);
                        }    
                    }

                }
            }
            if (hotHazardous.Count() > 0)
            {
                var farms = await GetAllFarmsWithFarmer(x => x.CropType == Domain.Enums.CropType.DrySeason && x.LocatedCity == location);
                foreach (var info in hotHazardous)
                {
                    if(farms.Count() > 0)
                    {
                        foreach (var farm in farms)
                        {
                            var weatherResponse = new WeatherResponse
                            {
                                Description = info.WeatherInformations[0].Description,
                                FarmerPhoneNumber = farm.Farmer.PhoneNumber,
                                FarmLocation = farm.LocatedCity,
                                FarmName = farm.FarmName,
                                DateOfIncidence = info.Dates.ForecastDate,
                                IncidenceType = "Hot Weather Hazard",
                                FarmerCountryCode = farm.Farmer.CountryPhoneCode,
                            };
                            weatherResponses.Add(weatherResponse);
                        }    
                    }
                    
                }
            }
          
        }
        return weatherResponses;
    }

}