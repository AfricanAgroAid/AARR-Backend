using Application.JSONResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Gateway.Services
{
    public class CityService : ICityService
    {
        private readonly IConfigurationSection _config;
        private readonly string _filePath;
        public CityService(IConfiguration configuration)
        {
            _config = configuration.GetSection("CitiesPath");
            _filePath = _config.GetSection("CitiesFilePath").Value;
        }
        public async Task<IList<City>> GetAllCitiesAsync()
        {
            var cityList = new List<City>();

            string text = await File.ReadAllTextAsync($"{_filePath}");
            
            var countries = JsonSerializer.Deserialize<IList<City>>(text,new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            foreach (City country in countries)
            {
                cityList.Add(country);
            }
            return cityList;
        }
        
    }
}
