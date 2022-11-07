using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Commands.Farmer.FarmerChangePhoneNumber
{
    internal class ChangeFarmerPhoneNumberCommand : ICommand<FarmerResponseModel>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
