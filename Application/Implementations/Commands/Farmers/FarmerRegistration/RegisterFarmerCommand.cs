using Application.Abstractions;

namespace Application.Implementations.Commands
{
    public class RegisterFarmerCommand : ICommand<FarmerResponseModel>
    {
        public string Name {get; set;}
        public string PhoneNumber {get; set;}
        public string Password {get; set;}
        public string Location {get; set;}
        public  string Language {get; set;}
    }
}