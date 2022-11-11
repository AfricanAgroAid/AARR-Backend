using System.Collections.Generic;

namespace Persistence.Implementation.Repositories;

public class WeatherResponse
{
          public List<string> Description{get; set;}
          public string FarmName{get;set;}
          public string FarmLocation{get; set;}
          public string FarmerPhoneNumber{get; set;}
}
