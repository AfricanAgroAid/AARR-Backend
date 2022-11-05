using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Farmers;

public class CreateFarmerRequestModel
{
    [Required]
    public string Name {get; set;}

    [Required]
    public string PhoneNumber {get; set;}

    [Required]
    public string Location {get; set;}
    
    [Required]
    public  string Language {get; set;}
}
