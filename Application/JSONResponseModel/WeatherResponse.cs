namespace  Application.JSONResponseModel;


public class WeatherResponse
{
    public string Description { get; set; }
    public string FarmName { get; set; }
    public string FarmLocation { get; set; }
    public string FarmerPhoneNumber { get; set; }
    public string FarmerCountryCode {get; set;}
    public DateTime DateOfIncidence{get; set;}
    public string IncidenceType {get; set;}
}

