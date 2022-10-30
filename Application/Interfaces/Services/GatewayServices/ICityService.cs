using Application.JSONResponseModel;
using System;

public interface ICityService
{
    Task<IList<City>> GetAllCitiesAsync();
}
